using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor;

public class Model : MonoBehaviour, IDamage
{
    [SerializeField] private float _maxLife = 100;
    private float _life;
    [SerializeField] private float _lifePerSeconds;
    private int points = 0;

    [Header("Input Keys")]
    public KeyCode UP;
    public KeyCode DOWN;
    public KeyCode LEFT;
    public KeyCode RIGHT;
    public KeyCode SHOOT;

    public GameManager gm;
    public Transform bulletSpawn;
    Rigidbody2D rb;

    public float speed;
    public Vector2 movDir;

    public bool isCooldown = false;
    public bool isInmortal = false;
    public bool isFreezeBullet = false;
    
    //public int reviveDuration;
    //public bool isKnocked = false;
    public bool isAlive = true;
    
    //public ReviveText reviveTimer;

    //public Bullet fireball;
    //public FreezeBullet freezeball;
    //public HUD Hud;

    IController _controller;
    View _view;

    // Por fuera del codigo solo puedo +=  o  -= funciones al Action.
    // No puedo ejecutar Actions [onDeath()], ni igualar Actions [onDeath = null]
    public event Action<float> onGetDmg = delegate { };
    public event Action<float> onHeal = delegate { };
    public event Action onDeath = delegate { };
    public event Action<int> onAddPoints = delegate { };
    public event Action<Vector2> onMove = delegate { };

    void Awake()
    {
        _life = _maxLife;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _view = GetComponent<View>();
        _controller = new Controller(this, _view, UP, DOWN, LEFT, RIGHT, SHOOT);
    }

    // Update is called once per frame
    void Update()
    {
        _controller.OnUpdate();
        Movement(movDir);
        HealthRegen();
    }

    public void Movement(Vector2 dir)
    {
        dir.Normalize();

        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * dir);

        onMove(dir);
    }

    public void TakeDamage(float dmg)
    {
        _life -= dmg;

        onGetDmg(_life / _maxLife);

        if (_life <= 0)
        {
            Death();
        }
    }

    public void AddPoints(int p)
    {
        points += p;
        onAddPoints(points);
    }

    private void HealthRegen()
    {
        if (isAlive)
        {
            _life += _lifePerSeconds * Time.deltaTime;
            onHeal(_life / _maxLife);
            if (_life > _maxLife)
            {
                _life = 100;
            }
        }
    }

    public int GetActualPoints()
    {
        return points;
    }

    public void Fire()
    {
        //bala del pool
        var p = ProjectileFactory.Instance.proyectilePool.GetObject();
        if (!p) return;

        p.transform.SetPositionAndRotation(bulletSpawn.position, bulletSpawn.rotation);
        p.m = this;
    }

    void Death()
    {
        Debug.Log("Mori");
        isAlive = false;
        onDeath();
    }
}
