using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PlayerOneController : Controller
{
    Model _m;
    View _v;

    public PlayerOneController(Model m, View v) : base(m, v)
    {
        _m = m;

        _m.onGetDmg += v.UpdateHudLife;
        //_m.onDeath += v.DeathMaterial;
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _m.Fire();
        }

        if (!_m.isKnocked)
        {

            if (Input.GetKey(KeyCode.W)) //Arriba
            {

                _m.movDir.y = 1;
                //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 180);
            }

            if (Input.GetKey(KeyCode.S)) //Abajo
            {

                _m.movDir.y = -1;
                //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.A)) //Izquierda
            {

                _m.movDir.x = -1;
                //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, -90);

                if (Input.GetKey(KeyCode.W))
                {

                    //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, -135);
                }

                if (Input.GetKey(KeyCode.S))
                {

                    //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, -45);
                }

            }

            if (Input.GetKey(KeyCode.D)) //Derecha
            {

                _m.movDir.x = 1;
                //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 90);

                if (Input.GetKey(KeyCode.W))
                {
                    //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 135);
                }
                if (Input.GetKey(KeyCode.S))
                {

                    //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 45);
                }
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) //Quieto Eje Y
            {
                _m.movDir.y = 0;
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) //Quieto Eje X
            {
                _m.movDir.x = 0;
            }
            if (Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.S))
            {

                _m.movDir.y = 0.5f;
            }

            if (Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.D))
            {

                _m.movDir.x = 0.5f;
            }

        }
    }
}
