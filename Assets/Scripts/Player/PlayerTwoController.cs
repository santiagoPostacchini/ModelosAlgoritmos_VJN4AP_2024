using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoController : Controller
{
    Model _m;

    public PlayerTwoController(Model m, View v) : base(m, v)
    {
        _m = m;

        _m.onGetDmg += v.UpdateHudLife;
        _m.onDeath += v.DeathMaterial;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
