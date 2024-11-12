using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float spawnRate;
    private int spawnChanceCompare;

    void Start()
    {
        spawnChanceCompare = Random.Range(0, 101);
        spawnRate = Random.Range(3, 6);
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new(spawnRate);

        while (GameManager.instance.eSpwner_canSpawn)
        {
            yield return wait;
            if (spawnChanceCompare < GameManager.instance.eSpwner_spawnChance)
            {
                Vector2 spawnPosition = new(
                    transform.position.x + Random.Range(-GameManager.instance.eSpwner_spawnRadius, GameManager.instance.eSpwner_spawnRadius),
                    transform.position.y + Random.Range(-GameManager.instance.eSpwner_spawnRadius, GameManager.instance.eSpwner_spawnRadius)
                );

                // Randomly choose a specific enemy prefab to spawn
                int randIndex = Random.Range(0, EnemyFactory.Instance.enemiesToPool.Length);
                var enemyPrefab = EnemyFactory.Instance.enemiesToPool[randIndex];

                // Get the specific enemy from the factory
                var enemy = EnemyFactory.Instance.GetEnemy(enemyPrefab);
                if (!enemy) yield return null;

                enemy.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            }
            spawnRate = Random.Range(3, 9);
            spawnChanceCompare = Random.Range(0, 101);
        }
    }
}