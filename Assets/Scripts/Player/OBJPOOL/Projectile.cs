using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Model m = null; //el que disparo
    public float _currentDistance;
    private float damage;
    //public GameObject HitEffect;

    private void Start()
    {
        damage = Random.Range(10f, 16f);
        this.GetComponent<Renderer>().sortingLayerName = "Default";
        this.GetComponent<Renderer>().sortingOrder = 3;
    }

    void Update()
    {
        float distanceToTravel = -GameManager.instance.p_speed * Time.deltaTime;
        _currentDistance += Mathf.Abs(distanceToTravel);

        transform.position += distanceToTravel * transform.up;

        if (_currentDistance > GameManager.instance.p_maxDistance)
        {
            Debug.Log("maxima distancia");
            ProjectileFactory.Instance.ReturnProjectile(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Instantiate(HitEffect, transform.position, Quaternion.identity);
        if (collision.gameObject.GetComponent<IDamage>() != null)
        {
            if (collision.gameObject.GetComponent<IEnemy>() != null)
            {
                if(m != null)
                {
                    collision.gameObject.GetComponent<IDamage>().TakeDamage(damage);
                    collision.gameObject.GetComponent<IEnemy>().AddPoints(m);
                    ProjectileFactory.Instance.ReturnProjectile(this);
                }
            }
            else //es un pj
            {
                if (m == null)
                {
                    collision.gameObject.GetComponent<IDamage>().TakeDamage(damage);
                    ProjectileFactory.Instance.ReturnProjectile(this);
                }
            }
        } 
        else if(collision.gameObject.tag == "Asset")
        {
            ProjectileFactory.Instance.ReturnProjectile(this);
        }
    }

    private void Reset()
    {
        _currentDistance = 0;
        damage = Random.Range(10f, 16f);
        m = null;
    }

    public static void TurnOnOff(Projectile p, bool active = true)
    {
        if (active) p.Reset();
        p.gameObject.SetActive(active);
    }
}
