using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionFactory : MonoBehaviour
{
    public static PotionFactory Instance;

    // Dictionary de pools segun el prefab de enemigo que quiera
    private Dictionary<Potion, ObjectPool<Potion>> enemyPools = new Dictionary<Potion, ObjectPool<Potion>>();
    public Potion[] potionsToPool;
    public int stonks = 10;
    public bool dynamic = true;

    void Start()
    {
        Instance = this;
        foreach (Potion p in potionsToPool)
        {
            if (!enemyPools.ContainsKey(p))
            {
                // Crear un pool si no existe una para ese enemigo
                enemyPools[p] = new ObjectPool<Potion>(() => InstantiatePotion(p), Potion.TurnOnOff, stonks, dynamic);
            }
        }
    }

    private Potion InstantiatePotion(Potion p)
    {
        return Instantiate(p, transform);
    }

    public Potion GetPotion(Potion p)
    {
        if (!enemyPools.ContainsKey(p))
        {
            enemyPools[p] = new ObjectPool<Potion>(() => InstantiatePotion(p), Potion.TurnOnOff, stonks, dynamic);
        }

        return enemyPools[p].GetObject();
    }

    public void ReturnPotion(Potion p)
    {
        // Encontrar la pool y traer el enemigo
        foreach (var poolEntry in enemyPools)
        {
            if (poolEntry.Key.GetType() == p.GetType())
            {
                poolEntry.Value.ReturnObject(p);
                return;
            }
        }
    }
}
