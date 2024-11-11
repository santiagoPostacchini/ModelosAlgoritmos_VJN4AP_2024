using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Controller : IController
{
    Model _m;
    View _v;

    KeyCode _up;
    KeyCode _down;
    KeyCode _left;
    KeyCode _right;
    KeyCode _shoot;
    KeyCode _rewind;

    public Controller(Model m, View v, KeyCode up, KeyCode down, KeyCode left, KeyCode right, KeyCode shoot, KeyCode rewind)
    {
        _m = m;

        _m.onGetDmg += v.UpdateHudLife;
        _m.onHeal += v.UpdateHudLife;
        _m.onAddPoints += v.UpdatePoints;
        _m.onDeath += v.DeathMaterial;
        _m.onDeath += () => GameManager.instance.PlayerDies(_m);
        _m.onMove += v.movAnimHandler;

        _up = up;
        _down = down;
        _left = left;
        _right = right;
        _shoot = shoot;
        _rewind = rewind;
    }

    public void OnUpdate()
    {
        if (_m.isAlive)
        {
            GetInput();
        }
        else
        {
            _m.movDir = Vector2.zero;
        }
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(_shoot))
        {
            _m.Fire();
        } 

        if(Input.GetKeyDown(_rewind)) 
        {
            _m.Rewind();
        }

        if (Input.GetKey(_up)) //Arriba
        {

            _m.movDir.y = 1;
            _m.bulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (Input.GetKey(_down)) //Abajo
        {

            _m.movDir.y = -1;
            _m.bulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(_left)) //Izquierda
        {

            _m.movDir.x = -1;
            _m.bulletSpawn.transform.rotation = Quaternion.Euler(0, 0, -90);

            if (Input.GetKey(_up))
            {

                _m.bulletSpawn.transform.rotation = Quaternion.Euler(0, 0, -135);
            }

            if (Input.GetKey(_down))
            {

                _m.bulletSpawn.transform.rotation = Quaternion.Euler(0, 0, -45);
            }

        }

        if (Input.GetKey(_right)) //Derecha
        {

            _m.movDir.x = 1;
            _m.bulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 90);

            if (Input.GetKey(_up))
            {
                _m.bulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 135);
            }
            if (Input.GetKey(_down))
            {
                _m.bulletSpawn.transform.rotation = Quaternion.Euler(0, 0, 45);
            }
        }

        if (Input.GetKeyUp(_up) || Input.GetKeyUp(_down)) //Quieto Eje Y
        {
            _m.movDir.y = 0;
        }

        if (Input.GetKeyUp(_left) || Input.GetKeyUp(_right)) //Quieto Eje X
        {
            _m.movDir.x = 0;
        }
        if (Input.GetKeyUp(_up) && Input.GetKeyUp(_down))
        {

            _m.movDir.y = 0.5f;
        }

        if (Input.GetKeyUp(_left) && Input.GetKeyUp(_right))
        {
            _m.movDir.x = 0.5f;
        }
    }
}
