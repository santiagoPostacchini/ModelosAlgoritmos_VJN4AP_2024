using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEffectPotion : Potion
{
    private Potion _potion;
    private Potion _secPotion;

    public DoubleEffectPotion(Potion potion, Potion secPotion)
    {
        _potion = potion;
        _secPotion = secPotion;
    }

    public override void Use(Model player)
    {
        _potion.Use(player);
        _secPotion.Use(player);
    }
}
