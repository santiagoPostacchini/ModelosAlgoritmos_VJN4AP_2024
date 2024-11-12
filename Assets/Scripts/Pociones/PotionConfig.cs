using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionConfig
{
    public string name { get; private set; }
    public Sprite potionSprite { get; private set; }
    public IPotionEffect[] effects { get; private set; }

    public PotionConfig(string name, Sprite sprite, IPotionEffect[] effects)
    {
        this.name = name;
        potionSprite = sprite;
        this.effects = effects;
    }
}