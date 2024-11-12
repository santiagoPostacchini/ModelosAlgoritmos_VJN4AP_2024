using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPotionEffect
{
    void ApplyEffect(Model player, int a = 0);
}

public class HealthEffect : IPotionEffect
{
    public void ApplyEffect(Model player, int life)
    {
        player.AddHealth(life);
    }
}

public class SpeedEffect : IPotionEffect
{
    public void ApplyEffect(Model player, int speed)
    {
        player.AddSpeed(speed);
    }
}

public class ShieldEffect : IPotionEffect
{
    public void ApplyEffect(Model player, int duration)
    {
        player.StartShield(duration);
    }
}