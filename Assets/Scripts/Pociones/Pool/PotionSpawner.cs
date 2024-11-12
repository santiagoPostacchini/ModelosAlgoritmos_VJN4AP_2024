using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public float timer = 0f;
    public float spawnRate;
    public int spawnChance;
    public int spawnChanceCompare;
    //public PotionSpawnEffect SpawnEffect;
    private readonly bool canSpawn = true;
    public float spawnRadius;

    void Start()
    {
        spawnRate = Random.Range(5, 11);
        StartCoroutine(Spawner()); // inicio una corutina que spawnea pociones aleatorios
    }

    private IEnumerator Spawner()
    {
        spawnRadius = 6f;
        spawnChance = 50;

        WaitForSeconds wait = new(spawnRate); // espera a que pase el tiempo aleatorio entre spawns

        while (canSpawn) // si se puede spawnear
        {
            yield return wait;
            if (spawnChanceCompare < spawnChance) // el 50% de las veces entra a spawnear la pocion
            {
                Vector2 spawnPosition = new(
                    transform.position.x + Random.Range(-spawnRadius, spawnRadius),
                    transform.position.y + Random.Range(-spawnRadius, spawnRadius)
                    ); //elije una posicion aleatoria de spawn dentro del radio

                int randPotion = Random.Range(0, PotionFactory.Instance.potionsToPool.Length);
                var potionPrefab = PotionFactory.Instance.potionsToPool[randPotion];

                var potion = PotionFactory.Instance.GetPotion(potionPrefab);
                if (!potion) yield return null;

                potion.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            }
            spawnRate = Random.Range(5, 11);
            spawnChanceCompare = Random.Range(0, 101);
        }
    }
}