using System.Collections;
using UnityEngine;
using System;

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
        if (remembering) return;
        dir.Normalize();

        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * dir);

        onMove(dir);
    }

    public void TakeDamage(float dmg)
    {
        if (!isInmortal)
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
                memento.Rec(new object[] { transform.position, transform.rotation, _life });
            }
            yield return new WaitForSeconds(recordInterval);
        }

    }

    protected override void BeRewind(ParamsMemento wrappers)
    {
        if (wrappers == null) return;

        Vector3 targetPosition = (Vector3)wrappers.parameters[0];
        Quaternion targetRotation = (Quaternion)wrappers.parameters[1];
        float targetHealth = (float)wrappers.parameters[2];

        StartCoroutine(LerpState(targetPosition, targetRotation, targetHealth));
    }

    private IEnumerator LerpState(Vector3 targetPosition, Quaternion targetRotation, float targetHealth)
    {
        float elapsedTime = 0f;

        // Valores actuales
        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;
        float initialHealth = _life;

        while (elapsedTime < timeBetweenMemories) //lerp lineal de toda la vida
        {
            float t = elapsedTime / timeBetweenMemories;

            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);
            _life = Mathf.Lerp(initialHealth, targetHealth, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Cuando termina los igualo para que no haya diferencias
        transform.position = targetPosition;
        transform.rotation = targetRotation;
        _life = targetHealth;
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
        yield return new WaitForSeconds(s * 2);
        speed -= s;
    }

    public void StartShield(int t)
    {
        StartCoroutine(ApplyShield(t));
    }
}
