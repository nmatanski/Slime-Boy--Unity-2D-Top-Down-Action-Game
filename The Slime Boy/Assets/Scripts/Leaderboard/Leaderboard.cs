using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private readonly string dreamloWebserviceURL = "http://dreamlo.com/lb/";
    private readonly string privateCode = "IjtBbluao0OPV9vE1xJH9AmH4ETfWHfEOTbcAuVOPBxA";
    //private readonly string publicCode = "IjtBbluao0OPV9vE1xJH9AmH4ETfWHfEOTbcAuVOPBxA"; ///TODO: Not used anywhere?

    private System.DateTime lastRequestTime = System.DateTime.Now;
    private int requestsCount = 0;
    private Leaderboard leaderboard;
    private PlayerPrefsController playerPrefsController;

    private struct Score
    {
        public string playerName;
        public int score;
        public int seconds;
        public string shortText;
        public string dateString;
    }


    public string HighScores { get; set; } = string.Empty;


    private void Start()
    {
        leaderboard = GetSceneLeaderboard();
        playerPrefsController = GetComponent<PlayerPrefsController>();

        var name = playerPrefsController.GetName();
        if (string.IsNullOrWhiteSpace(name)) /// || playerPrefsController.GetScore() == 0 but it won't update it tho
        {
            return;
        }

        AddScore(name);
    }

    private void OnGUI()
    {
        PrintLeaderboardGUI();
    }

    public void AddScore(string playerName, int totalScore)
    {
        if (TooManyRequests()) return;

        StartCoroutine(AddScoreWithPipe(playerName, totalScore));
    }

    public void AddScore(string playerName)
    {
        if (TooManyRequests()) return;

        StartCoroutine(AddScoreWithPipe(playerName));
    }

    private void PrintLeaderboardGUI()
    {
        var width200 = new GUILayoutOption[] { GUILayout.Width(200) };

        float width = 400;  // Make this wider to add more columns
        float height = 200;

        var r = new Rect((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height), width, height);
        GUILayout.BeginArea(r, new GUIStyle("box"));

        GUILayout.BeginVertical();

        GUILayout.Label("High Scores:");
        var scoreList = leaderboard.ToListHighToLow();

        if (scoreList == null)
        {
            GUILayout.Label("(loading...)");
        }
        else
        {
            int maxToDisplay = 20;
            int count = 0;
            foreach (var currentScore in scoreList)
            {
                count++;
                GUILayout.BeginHorizontal();
                GUILayout.Label(currentScore.playerName, width200);
                GUILayout.Label(currentScore.score.ToString(), width200);
                GUILayout.EndHorizontal();

                if (count >= maxToDisplay) break;
            }
        }

        GUILayout.EndArea();
    }

    private bool TooManyRequests()
    {
        var now = System.DateTime.Now;

        if (DateDiffInSeconds(now, lastRequestTime) <= 2)
        {
            lastRequestTime = now;
            requestsCount++;
            if (requestsCount > 3)
            {
                Debug.LogError("DREAMLO Too Many Requests. Am I inside an update loop?");
                return true;
            }
        }
        else
        {
            lastRequestTime = now;
            requestsCount = 0;
        }

        return false;
    }

    private List<Score> ToListLowToHigh()
    {
        var scoreList = ToScoreArray();

        if (scoreList == null)
            return new List<Score>();

        var genericList = new List<Score>(scoreList);
        genericList.Sort((x, y) => x.score.CompareTo(y.score));

        return genericList;
    }

    private List<Score> ToListHighToLow()
    {
        var scoreList = ToScoreArray();

        if (scoreList == null)
            return new List<Score>();

        var genericList = new List<Score>(scoreList);
        genericList.Sort((x, y) => y.score.CompareTo(x.score));

        return genericList;
    }

    private Score[] ToScoreArray()
    {
        string[] rows = ToStringArray();
        if (rows == null)
            return null;

        int rowcount = rows.Length;
        if (rowcount <= 0)
            return null;

        var scoreList = new Score[rowcount];

        for (int i = 0; i < rowcount; i++)
        {
            string[] values = rows[i].Split(new char[] { '|' }, System.StringSplitOptions.None);

            var current = new Score
            {
                playerName = values[0],
                score = 0,
                seconds = 0,
                shortText = "",
                dateString = ""
            };

            if (values.Length > 1)
                current.score = CheckInt(values[1]);

            if (values.Length > 2)
                current.seconds = CheckInt(values[2]);

            if (values.Length > 3)
                current.shortText = values[3];

            if (values.Length > 4)
                current.dateString = values[4];

            scoreList[i] = current;
        }

        return scoreList;
    }

    private string[] ToStringArray()
    {
        if (string.IsNullOrEmpty(HighScores))
            return null;

        return HighScores.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
    }

    private static int CheckInt(string s)
    {
        int.TryParse(s, out int x);
        return x;
    }

    // This function saves a trip to the server. Adds the score and retrieves results in one trip.
    private IEnumerator AddScoreWithPipe(string playerName, int totalScore = -1)
    {
        playerName = Clean(playerName);

        if (totalScore < 0 && PlayerPrefs.HasKey("PlayerScore"))
        {
            totalScore = playerPrefsController.GetScore();
        }

        totalScore = Mathf.Clamp(totalScore, 0, int.MaxValue);

#pragma warning disable CS0618 // Type or member is obsolete -> The UnityRequest class doesn't have a text value
        var www = new WWW(dreamloWebserviceURL + privateCode + "/add-pipe/" + WWW.EscapeURL(playerName) + "/" + totalScore.ToString());
#pragma warning restore CS0618 // Type or member is obsolete
        yield return www;
        HighScores = www.text;
    }

    public static Leaderboard GetSceneLeaderboard()
    {
        var board = GameObject.FindGameObjectWithTag("Leaderboard");

        if (board == null)
        {
            Debug.LogError("Could not find leaderboard prefab instantiated in the scene.");
            return null;
        }
        return board.GetComponent<Leaderboard>();
    }

    public static double DateDiffInSeconds(System.DateTime now, System.DateTime olderdate)
    {
        return now.Subtract(olderdate).TotalSeconds;
    }

    private static string Clean(string s)
    {
        s = s.Replace("/", "");
        s = s.Replace("|", "");
        return s;
    }
}
