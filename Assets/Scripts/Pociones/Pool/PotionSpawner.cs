using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    private float spawnRate;
    [SerializeField] private float spawnRadius = 6f;

    private void Start()
    {
        spawnRate = Random.Range(5, 11);
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new(spawnRate);

        while (true)
        {
            yield return wait;

            if (Random.Range(0, 101) < 50)
            {
                Vector2 spawnPosition = new Vector2(
                    transform.position.x + Random.Range(-spawnRadius, spawnRadius),
                    transform.position.y + Random.Range(-spawnRadius, spawnRadius)
                );

                var p = PotionFactory.Instance.GetPotion();

                if (p != null)
                {
                    p.transform.position = spawnPosition;
                    p.gameObject.SetActive(true);
                }
            }

            spawnRate = Random.Range(5, 11);
        }
    }
}