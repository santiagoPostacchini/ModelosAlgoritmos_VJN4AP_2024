using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Model : MonoBehaviour
{
    public float baseHp = 100;
    public float _currentHp;

    IController _myController;
    View _view;

    // Por fuera del codigo solo puedo +=  o  -= funciones al Action.
    // No puedo ejecutar Actions [onDeath()], ni igualar Actions [onDeath = null]
    public event Action<float> onGetDmg = delegate { };
    public event Action onDeath = delegate { };

    void Awake()
    {
        _currentHp = baseHp;
    }

    private void Start()
    {
        _view = GetComponent<View>();
        _myController = new PlayerOneController(this, _view);
    }

    // Update is called once per frame
    void Update()
    {
        _myController.OnUpdate();
    }

    public void Movement(Vector3 direction)
    {
        transform.position += direction * 2 * Time.deltaTime;
    }
    public void TakeDamage(float dmg) 
    {
        _currentHp -= dmg;

        onGetDmg(_currentHp / baseHp);

        if (_currentHp <= 0) 
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Mori");
        onDeath();
    }
}
