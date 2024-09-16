using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance;

    public Enemy
        enemyPrefab;
    public int stonks = 10;
    public bool dynamic = true;

    public ObjectPool<Enemy> enemyPool;

    void Start()
    {
        Instance = this;
        enemyPool = new ObjectPool<Enemy>(EnemyCreator, Enemy.TurnOnOff, stonks, dynamic);
    }

    public Enemy EnemyCreator()
    {
        return Instantiate(enemyPrefab, transform);
    }

    public void ReturnEnemy(Enemy e)
    {
        enemyPool.ReturnObject(e);
    }
}
