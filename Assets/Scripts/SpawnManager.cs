using UnityEngine;

using System.Collections;


public class SpawnManager : MonoBehaviour

{

    public GameObject[] enemyPrefabs;

    public Transform[] spawnPoints;


    public int maxEnemies = 10;

    private int currentEnemyCount = 0;

    private int waveNumber = 1;


    private float spawnInterval = 5f; // Time between spawns

    private float difficultyMultiplier = 1.0f; // Multiplier for scaling difficulty


    private void Start()

    {

        StartCoroutine(SpawnEnemiesRoutine());

    }


    private IEnumerator SpawnEnemiesRoutine()

    {

        while (true)

        {

            if (currentEnemyCount < maxEnemies)

            {

                SpawnEnemy();

            }


            yield return new WaitForSeconds(spawnInterval / difficultyMultiplier);

        }

    }


    private void SpawnEnemy()

    {

        int randomIndex = Random.Range(0, enemyPrefabs.Length);

        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);


        GameObject enemy = Instantiate(enemyPrefabs[randomIndex], spawnPoints[randomSpawnPoint].position, Quaternion.identity);

        Enemy enemyScript = enemy.GetComponent<Enemy>();


        if (enemyScript != null)

        {

            enemyScript.OnDestroyed += HandleEnemyDestroyed;

            enemyScript.ScaleAttributes(difficultyMultiplier);

        }


        currentEnemyCount++;

    }


    private void HandleEnemyDestroyed()

    {

        currentEnemyCount--;


        if (currentEnemyCount <= 0)

        {

            IncreaseDifficulty();

        }

    }


    private void IncreaseDifficulty()

    {

        waveNumber++;

        difficultyMultiplier += 0.2f;

        maxEnemies += 2;

        spawnInterval = Mathf.Max(1f, spawnInterval - 0.5f);

        Debug.Log($"Wave {waveNumber} started! Difficulty Multiplier: {difficultyMultiplier}, Spawn Interval: {spawnInterval}s");

    }

}