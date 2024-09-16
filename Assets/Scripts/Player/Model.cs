using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Model : MonoBehaviour, IDamage
{
    [SerializeField] private float _maxLife = 100;
    private float _life;
    private float lifePerSeconds;

    public GameManager gm;
    public Transform bulletSpawn;
    Rigidbody2D rb;

    public float speed;
    public Vector2 movDir;

    public bool isCooldown = false;
    public bool isInmortal = false;
    public bool isFreezeBullet = false;
    
    //public int reviveDuration;
    public bool isKnocked;
    
    //public ReviveText reviveTimer;

    //public Bullet fireball;
    //public FreezeBullet freezeball;
    //public HUD Hud;

    IController _myController;
    View _view;

    // Por fuera del codigo solo puedo +=  o  -= funciones al Action.
    // No puedo ejecutar Actions [onDeath()], ni igualar Actions [onDeath = null]
    public event Action<float> onGetDmg = delegate { };
    public event Action onDeath = delegate { };

    void Awake()
    {
        _life = _maxLife;
        rb = GetComponent<Rigidbody2D>();
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
        Movement(movDir);
    }

    public void Movement(Vector2 dir)
    {
        dir.Normalize();

        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * dir);
    }

    public void TakeDamage(float dmg, string id = null)
    {
        _life -= dmg;

        onGetDmg(_life / _maxLife);

        if (_life <= 0)
        {
            Death();
        }
    }

    public void Fire()
    {
        //bala del pool
        var p = ProjectileFactory.Instance.proyectilePool.GetObject();
        if (!p) return;

        p.transform.SetPositionAndRotation(bulletSpawn.position, Quaternion.identity);
    }

    void Death()
    {
        Debug.Log("Mori");
        onDeath();
    }
}
