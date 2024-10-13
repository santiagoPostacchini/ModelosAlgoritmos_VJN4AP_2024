using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance;

    // Dictionary de pools segun el prefab de enemigo que quiera
    private Dictionary<Enemy, ObjectPool<Enemy>> enemyPools = new Dictionary<Enemy, ObjectPool<Enemy>>();
    public Enemy[] enemiesToPool;
    public int stonks = 10;
    public bool dynamic = true;

    void Start()
    {
        Instance = this;
        foreach (Enemy e in enemiesToPool)
        {
            if (!enemyPools.ContainsKey(e))
            {
                // Crear un pool si no existe una para ese enemigo
                enemyPools[e] = new ObjectPool<Enemy>(() => InstantiateEnemy(e), Enemy.TurnOnOff, stonks, dynamic);
            }
        }
    }

    private Enemy InstantiateEnemy(Enemy e)
    {
        return Instantiate(e, transform);
    }

    public Enemy GetEnemy(Enemy e)
    {
        if (!enemyPools.ContainsKey(e))
        {
            enemyPools[e] = new ObjectPool<Enemy>(() => InstantiateEnemy(e), Enemy.TurnOnOff, stonks, dynamic);
        }

        return enemyPools[e].GetObject();
    }

    public void ReturnEnemy(Enemy e)
    {
        // Encontrar la pool y traer el enemigo
        foreach (var poolEntry in enemyPools)
        {
            if (poolEntry.Key.GetType() == e.GetType())
            {
                poolEntry.Value.ReturnObject(e);
                return;
            }
        }
    }
}