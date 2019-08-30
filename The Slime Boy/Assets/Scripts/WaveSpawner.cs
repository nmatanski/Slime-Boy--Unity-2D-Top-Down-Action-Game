using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private Wave currentWave;

    [SerializeField]
    private int currentWaveIndex;

    private Transform player;

    private bool hasFinishedSpawning;

    private TextMeshProUGUI tooltip;


    [SerializeField]
    private List<Wave> waves;

    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private float timeBetweenWaves;

    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private Transform bossSpawnPoint; // at the center of the camera


    private void Start()
    {
        tooltip = GameObject.FindGameObjectWithTag("TooltipUI").GetComponent<TextMeshProUGUI>();
        tooltip.text = "Level 1\nMeet Spikey";
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    private void Update()
    {
        if (hasFinishedSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            hasFinishedSpawning = false;

            if (currentWaveIndex + 1 < waves.Count) // index + 1 < waves.count ??
            {
                currentWaveIndex++;
                //change background color
                var bg = GameObject.FindGameObjectWithTag("Background");
                var text = "";
                switch (currentWaveIndex)
                {
                    case 1:
                        text = "Level 1\nSpikey is angry! He can leap now.";
                        break;
                    case 2:
                        text = "Level 1\nLearn everything about Spikey.";
                        break;
                    case 4:
                        text = "Level 1\nMeet... fireballs.";
                        break;
                    case 5:
                        text = "Level 1\nLearn! Learn! Learn!";
                        break;
                    case 9:
                        text = "Level 1\nMeet Summoner!";
                        break;
                    case 10:
                        text = "Level 1: THE END\nLearn EVERYTHING!";
                        break;
                    case 11:
                        bg.GetComponent<SpriteRenderer>().color = Color.cyan;
                        text = "Level 2\nSwarm time! RUN! RUN! RUN!";
                        break;
                    case 12:
                        text = "Level 2\nDid you learn everything? Let's check your skills now. :)";
                        break;
                    case 13:
                        text = "Level 2\nGood job! You can rest now.";
                        break;
                    case 14:
                        bg.GetComponent<SpriteRenderer>().color = Color.blue;
                        text = "Level 3\nThe never ending towers.";
                        break;
                    case 15:
                        text = "Level 3\nThis hurts! ;[";
                        break;
                    case 16:
                        bg.GetComponent<SpriteRenderer>().color = new Color(255, 60, 187); // deep pink
                        text = "Watch for falling objects!";
                        break;
                        //case 5:
                        //    bg.GetComponent<SpriteRenderer>().color = new Color(15, 165, 140);
                        //    break;
                }
                ChangeText(tooltip, text);
                //end
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                var spawnPoint = bossSpawnPoint.position;
                spawnPoint.z = 0;
                Instantiate(boss, spawnPoint, bossSpawnPoint.rotation).SetActive(true);
                Debug.Log("Game Over");
                Character.FlashInput(Color.green);
            }
        }
    }

    public void ChangeText(TextMeshProUGUI textbox, string text)
    {
        StartCoroutine(textbox.ChangeText(text));
    }

    public void ChangeText(string text, System.Func<string, IEnumerator> changeText)
    {
        StartCoroutine(changeText(text));
    }

    private IEnumerator StartNextWave(int waveIndex)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(waveIndex));
    }

    private IEnumerator SpawnWave(int waveIndex)
    {
        currentWave = waves[waveIndex];

        for (int i = 0; i < currentWave.Count; i++)
        {
            if (player == null)
            {
                yield break;
            }

            var randomEnemy = currentWave.Enemies[Random.Range(0, currentWave.Enemies.Count)];
            var randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Instantiate(randomEnemy, randomSpawnPoint.position, randomSpawnPoint.rotation);

            hasFinishedSpawning = i == currentWave.Count - 1;

            yield return new WaitForSeconds(currentWave.TimeBetweenSpawns);
        }

        //if (waveIndex + 1 == waves.Count)
        //{
        //    hasFinishedSpawning = true;
        //}
    }
}
