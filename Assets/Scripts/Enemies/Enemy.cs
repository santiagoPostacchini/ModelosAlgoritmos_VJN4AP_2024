using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemy
{

    protected virtual void Reset()
    {
        
    }

    public static void TurnOnOff(Enemy e, bool active = true)
    {
        if (active) e.Reset();
        e.gameObject.SetActive(active);
    }

    public virtual void AddPoints(string id)
    {

    }
}
