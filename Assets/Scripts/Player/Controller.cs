using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : IController
{
    Model _m;
    //View _v;

    public Controller(Model m, View v)
    {
        _m = m;

        _m.onGetDmg += v.UpdateHudLife;
        _m.onDeath += v.DeathMaterial;
    }

    public virtual void OnUpdate()
    {
        
    }
}
