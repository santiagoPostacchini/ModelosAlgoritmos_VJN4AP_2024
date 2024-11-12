using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PotionFactory : MonoBehaviour
{
    public static PotionFactory Instance;

    private ObjectPool<Potion> potionPool;
    public int stonks = 10;
    public bool dynamic = true;

    public Sprite[] potionSprites;

    private void Awake()
    {
        Instance = this;
        potionPool = new ObjectPool<Potion>(CreatePotion, Potion.TurnOnOff, stonks, dynamic);
    }

    private Potion CreatePotion()
    {
        GameObject potionObject = new GameObject("Potion");
        BasicPotion potion = potionObject.AddComponent<BasicPotion>();
        potionObject.AddComponent<SpriteRenderer>();

        CircleCollider2D collider = potionObject.AddComponent<CircleCollider2D>();

        potionObject.transform.SetParent(transform);

        return potion;
    }

    public Potion GetPotion()
    {
        Potion potion = potionPool.GetObject();

        if (potion is BasicPotion basicPotion) // Te decoro
        {
            basicPotion.SetEffects(GetRandomEffects());
            SpriteRenderer renderer = basicPotion.GetComponent<SpriteRenderer>();
            renderer.sprite = GetRandomSprite();
        }

        return potion;
    }

    public void ReturnPotion(Potion potion)
    {
        if (potion is BasicPotion basicPotion) //Te desdecoro ahre
        {
            basicPotion.ClearEffects();
        }

        potionPool.ReturnObject(potion);
    }

    private IPotionEffect[] GetRandomEffects()
    {
        List<IPotionEffect> allEffects = new List<IPotionEffect>
        {
            new HealthEffect(),
            new SpeedEffect(),
            new ShieldEffect()
        };

        List<IPotionEffect> selectedEffects = new List<IPotionEffect>();
        int effectCount = Random.Range(1, allEffects.Count + 1);

        while (selectedEffects.Count < effectCount)
        {
            int randomIndex = Random.Range(0, allEffects.Count);
            IPotionEffect randomEffect = allEffects[randomIndex];
            if (!selectedEffects.Contains(randomEffect))
            {
                selectedEffects.Add(randomEffect);
            }
        }

        return selectedEffects.ToArray();
    }

    private Sprite GetRandomSprite()
    {
        int randomIndex = Random.Range(0, potionSprites.Length);
        return potionSprites[randomIndex];
    }
}
