using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potion : MonoBehaviour
{
    Vector2 posOrigin = new Vector2();
    Vector2 tempPos = new Vector2();
    private float amplitude = 0.5f;
    private float frecuency = 1f;

    protected virtual void Start()
    {
        posOrigin = transform.position;
    }
    public abstract void Use(Model player);

    public virtual void Reset()
    {

    }

    public static void TurnOnOff(Potion p, bool active = true)
    {
        if (active) p.Reset();
        p.gameObject.SetActive(active);
    }

    private void Update()
    {
        MovePot();
    }

    void MovePot()
    {
        tempPos = posOrigin;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frecuency) * amplitude;
        transform.position = tempPos;
    }

    private void OnCollisionEnter2D(Collision2D collision) //si colisiona con algo (lo que sea)
    {

        if (collision.gameObject.GetComponent<Model>()) //si eso es un jugador
        {
            Model p = collision.gameObject.GetComponent<Model>();
            Use(p);
            PotionFactory.Instance.ReturnPotion(this);
        }
    }
}