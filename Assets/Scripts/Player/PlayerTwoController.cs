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
        //_m.onDeath += v.DeathMaterial;
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _m.Fire();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _m.movDir.y = 1;
                //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 180);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                _m.movDir.y = -1;
                //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.LeftArrow)) //Izquierda
            {
                _m.movDir.x = -1;
                //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, -90);

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, -135);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, -45);
                }

            }

            if (Input.GetKey(KeyCode.RightArrow)) //Derecha
            {
                _m.movDir.x = 1;
                //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 90);

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 135);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 45);
                }
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                _m.movDir.y = 0;
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                _m.movDir.x = 0;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) && Input.GetKeyUp(KeyCode.DownArrow))
            {
                _m.movDir.y = 0.5f;
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKeyUp(KeyCode.RightArrow))
            {
                _m.movDir.x = 0.5f;
            }
            _m.movDir.y = 1;
            //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _m.movDir.y = -1;
            //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) //Izquierda
        {
            _m.movDir.x = -1;
            //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, -90);

            if (Input.GetKey(KeyCode.UpArrow))
            {
                //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, -135);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, -45);
            }

        }

        if (Input.GetKey(KeyCode.RightArrow)) //Derecha
        {
            _m.movDir.x = 1;
            //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 90);

            if (Input.GetKey(KeyCode.UpArrow))
            {
                //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 135);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                //BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 45);
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            _m.movDir.y = 0;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _m.movDir.x = 0;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && Input.GetKeyUp(KeyCode.DownArrow))
        {
            _m.movDir.y = 0.5f;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKeyUp(KeyCode.RightArrow))
        {
            _m.movDir.x = 0.5f;
        }
    }
}
