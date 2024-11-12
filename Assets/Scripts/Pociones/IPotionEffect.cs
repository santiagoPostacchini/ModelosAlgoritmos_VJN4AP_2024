using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPotionEffect
{
    void ApplyEffect(Model player);
}

public class HealthEffect : IPotionEffect
{
    public void ApplyEffect(Model player)
    {
        player.AddHealth(player.lifeToAdd);
    }
}

public class SpeedEffect : IPotionEffect
{
    public void ApplyEffect(Model player)
    {
        player.AddSpeed(player.speedToAdd);
    }
}

public class ShieldEffect : IPotionEffect
{
    public void ApplyEffect(Model player)
    {
        player.StartShield(player.shieldDuration);
    }
}