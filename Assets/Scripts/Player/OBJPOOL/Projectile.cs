using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 7f, _maxDistance = 300f, bulletDamage, _currentDistance = 0f;

    public string pID;
    public string playerName;
    public GameObject HitEffect;

    private void Start()
    {
        bulletDamage = Random.Range(10f, 16f);
    }

    void Update()
    {
        float distanceToTravel = -_speed * Time.deltaTime;
        _currentDistance += distanceToTravel;

        if (_currentDistance > _maxDistance)
        {
            ProjectileFactory.Instance.ReturnProjectile(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Instantiate(HitEffect, transform.position, Quaternion.identity);
        if (collision.gameObject.GetComponent<IDamage>() != null) //si eso puede dañarse
        {
            if(collision.gameObject.GetComponent<IEnemy>() != null) //y es un enemigo
            {
                collision.gameObject.GetComponent<IDamage>().TakeDamage(bulletDamage, pID);
                collision.gameObject.GetComponent<IEnemy>().AddPoints(pID);
                ProjectileFactory.Instance.ReturnProjectile(this);
            }
            else
            {
                collision.gameObject.GetComponent<IDamage>().TakeDamage(bulletDamage);
                ProjectileFactory.Instance.ReturnProjectile(this);
            }

        }
        ProjectileFactory.Instance.ReturnProjectile(this); //si choca con algo igual se destruye
    }

    private void Reset()
    {
        bulletDamage = Random.Range(10f, 16f);
    }

    public static void TurnOnOff(Projectile p, bool active = true)
    {
        if (active) p.Reset();
        p.gameObject.SetActive(active);
    }
}
