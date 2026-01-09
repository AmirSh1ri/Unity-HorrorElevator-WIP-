using UnityEngine;
using System.Collections;

// Enemy Spawner Logic used only in Subway for now (The other rooms don't need spawning)
public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    public int maxTotal = 12;

    private int spawned = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (spawned < maxTotal)
        {
            float waitTime = Random.Range(2f, 6f);
            yield return new WaitForSeconds(waitTime);

            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            Instantiate(prefab, spawn.position, spawn.rotation);
            spawned++;
        }
    }
}
