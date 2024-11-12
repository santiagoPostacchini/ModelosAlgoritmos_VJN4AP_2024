using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPotion : Potion
{
    private IPotionEffect _effect;

    public BasicPotion(IPotionEffect effect)
    {
        _effect = effect;
    }

    public override void Use(Model player)
    {
        _effect.ApplyEffect(player);
    }
}
