using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private Wave currentWave;

    private int currentWaveIndex;

    private Transform player;

    private bool hasFinishedSpawning;


    [SerializeField]
    private List<Wave> waves;

    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private float timeBetweenWaves;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    private void Update()
    {
        if (hasFinishedSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            hasFinishedSpawning = false;
            if (currentWaveIndex + 1 < waves.Count)
            {
                currentWaveIndex++;
                //change background color
                var bg = GameObject.FindGameObjectWithTag("Background");
                switch (currentWaveIndex)
                {
                    case 1:
                        bg.GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    case 2:
                        bg.GetComponent<SpriteRenderer>().color = Color.blue;
                        break;
                }
                //end
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                Debug.Log("Game Over");
                Character.FlashInput(Color.green);
            }
        }
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
    }
}
