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
        //tooltip.text = waves[currentWaveIndex].TooltipText;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartNextWave(currentWaveIndex);
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
                switch (currentWaveIndex)
                {
                    case 11:
                        bg.GetComponent<SpriteRenderer>().color = Color.cyan;
                        break;
                    case 14:
                        bg.GetComponent<SpriteRenderer>().color = Color.blue;
                        break;
                    case 16:
                        bg.GetComponent<SpriteRenderer>().color = new Color(255, 60, 187); // deep pink
                        break;
                        //case 5:
                        //    bg.GetComponent<SpriteRenderer>().color = new Color(15, 165, 140);
                        //    break;
                }
                //end
                StartNextWave(currentWaveIndex);
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

    private void StartNextWave(int waveIndex)
    {
        StartCoroutine(SpawnWave(waveIndex));
    }

    private IEnumerator SpawnWave(int waveIndex)
    {
        currentWave = waves[waveIndex];
        ChangeText(tooltip, currentWave.TooltipText);

        yield return new WaitForSeconds(timeBetweenWaves);


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
    }
}
