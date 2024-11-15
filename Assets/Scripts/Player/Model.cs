using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor;

public class Model : Rewind, IDamage
{
    [SerializeField] private float _maxLife = 100;
    [SerializeField] private float _life;
    [SerializeField] private float _lifePerSeconds;
    private int points = 0;

    
    public KeyCode UP;
    public KeyCode DOWN;
    public KeyCode LEFT;
    public KeyCode RIGHT;
    public KeyCode SHOOT;
    public KeyCode REWIND;

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

    [Header("Potion Effects")]
    public GameObject shieldEffect;
    public int shieldDuration = 5;
    public int lifeToAdd = 20;
    public float speedToAdd = 3;

    IController _controller;
    View _view;

    public event Action<float> onGetDmg = delegate { };
    public event Action<float> onHeal = delegate { };
    public event Action onDeath = delegate { };
    public event Action<int> onAddPoints = delegate { };
    public event Action<Vector2> onMove = delegate { };
    public event Action onPotionEffect = delegate { };

    public override void Awake()
    {
        _life = _maxLife;
        base.Awake();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _view = GetComponent<View>();
        _controller = new Controller(this, _view, UP, DOWN, LEFT, RIGHT, SHOOT, REWIND);
    }

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
        if(!isInmortal)
        {
            _life -= dmg;

            onGetDmg(_life / _maxLife);

            if (_life <= 0)
            {
                Death();
            }
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

    public void Rewind()
    {
        ManagerMemento.instance.GoBack(this);
    }

    public override IEnumerator StartToRec()
    {
        while (true)
        {
            if (!remembering)
            {
                memento.Rec(new object[] { transform.position, transform.rotation, _life, isAlive });
            }
            else
            {
                Debug.Log("NO RECORDANDO");
            }
            yield return new WaitForSeconds(recordInterval);
        }
    }

    protected override void BeRewind(ParamsMemento wrappers)
    {
        _life = (float)wrappers.parameters[2];
        isAlive = (bool)wrappers.parameters[3];
        transform.position = (Vector3)wrappers.parameters[0];
        transform.rotation = (Quaternion)wrappers.parameters[1];
    }

    public void AddHealth(int l)
    {
        _life += l;
        onHeal(_life / _maxLife);
        if (_life > _maxLife)
        {
            _life = 100;
        }
        Debug.Log($"Heal effect, actual life: {_life}");
    }

    private IEnumerator ApplyShield(int time)
    {
        Debug.Log("Empezo escudo");
        isInmortal = true;
        shieldEffect.SetActive(true);
        yield return new WaitForSeconds(time);
        shieldEffect.SetActive(false);
        isInmortal = false;
        Debug.Log("Termino escudo");
    }
    public void StartSpeed(float t)
    {
        StartCoroutine(AddSpeed(t));
    }

    public IEnumerator AddSpeed(float s)
    {
        speed += s;
        Debug.Log($"Speed effect, actual speed: {speed}");
        yield return new WaitForSeconds(s*2);
        speed -= s;
    }

    public void StartShield(int t)
    {
        StartCoroutine(ApplyShield(t));
    }


    #region INTERPOLATION
    /*private void PerformInterpolation()
    {
        if (_rememberParams.Count < 2) return;  // Necesitamos al menos 2 estados para hacer la interpolación

        // Recorremos todos los pares consecutivos de estados
        for (int i = 0; i < _rememberParams.Count - 1; i++)
        {
            ParamsMemento initialState = _rememberParams[i];  // Estado actual
            ParamsMemento finalState = _rememberParams[i + 1];  // Estado siguiente

            // Normalizamos el tiempo de interpolación entre 0 y 1
            _interpolationTime += Time.deltaTime;

            float t = Mathf.Clamp01(_interpolationTime / _interpolationDuration);

            // Interpolamos entre los valores de los estados (posición, rotación, salud, estado de vida)
            for (int j = 0; j < initialState.parameters.Length; j++)
            {
                // Verificamos el tipo de cada parámetro y realizamos la interpolación
                if (initialState.parameters[j] is Vector3)
                {
                    transform.position = Vector3.Lerp((Vector3)initialState.parameters[j], (Vector3)finalState.parameters[j], t);
                }
                else if (initialState.parameters[j] is Quaternion)
                {
                    transform.rotation = Quaternion.Lerp((Quaternion)initialState.parameters[j], (Quaternion)finalState.parameters[j], t);
                }
                else if (initialState.parameters[j] is float)
                {
                    // Interpolando la salud
                    _life = Mathf.Lerp((float)initialState.parameters[j], (float)finalState.parameters[j], t);
                }
                else if (initialState.parameters[j] is bool)
                {
                    // Interpolando el estado de vida (si está vivo o muerto)
                    isAlive = (bool)finalState.parameters[j];
                }
            }

            // Si el tiempo de interpolación ha pasado para este par, continuamos al siguiente par
            if (t >= 1f)
            {
                // Reiniciamos el tiempo de interpolación para el siguiente par
                _interpolationTime = 0f;
            }
        }
    }*/
    #endregion
}
