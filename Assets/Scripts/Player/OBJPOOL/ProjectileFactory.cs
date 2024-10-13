using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    public static ProjectileFactory Instance;

    public Projectile projectilePrefab;
    public int stonks = 10;
    public bool dynamic = true;

    public ObjectPool<Projectile> proyectilePool;

    void Start()
    {
        Instance = this;
        proyectilePool = new ObjectPool<Projectile>(ProjectileCreator, Projectile.TurnOnOff, stonks, dynamic);
    }

    public Projectile ProjectileCreator()
    {
        return Instantiate(projectilePrefab, transform);
    }

    public void ReturnProjectile(Projectile p)
    {
        proyectilePool.ReturnObject(p);
    }
}
