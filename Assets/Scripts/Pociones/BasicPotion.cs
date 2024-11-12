using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPotion : Potion
{
    private List<IPotionEffect> _effects = new List<IPotionEffect>();

    public void SetEffects(params IPotionEffect[] effects)
    {
        _effects = new List<IPotionEffect>(effects);
    }

    public override void Use(Model player)
    {
        foreach (var effect in _effects)
        {
            effect.ApplyEffect(player);
        }
    }

    public void ClearEffects()
    {
        _effects.Clear();
    }
}
