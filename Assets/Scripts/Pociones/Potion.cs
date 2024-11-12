using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Potion : MonoBehaviour
{
    Vector2 posOrigin = new Vector2();
    Vector2 tempPos = new Vector2();
    private float amplitude = 0.5f;
    private float frecuency = 1f;

    public PotionConfig config { get; private set; }

    protected virtual void Start()
    {
        posOrigin = transform.position;
        transform.localScale = new Vector3(.1f,.1f,.1f);
        this.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        this.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    public void Initialize(PotionConfig config)
    {
        this.config = config;
        GetComponent<SpriteRenderer>().sprite = config.potionSprite;
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