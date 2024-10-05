using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlower : Enemy, IDamage
{
    // Variables públicas
    //public FreezeEffectScript freezeEffect; // Prefab del efecto de congelamiento
    //FreezeEffectScript newFreezeEffect;
    [SerializeField] private Transform _bulletDir;
    public float freezeTime = 0;
    private float _life = 0;

    // Variables privadas
    private float timer; // Contador para el ataque
    private bool isAttacking = false; // Estado de ataque
    //private bool isFreezed; // Estado de freeze

    // Inicialización
    void Start()
    {
        // Inicializar el contador y el estado de ataque
        timer = 0f;
        //isFreezed = false;
        freezeTime = 0;
        this.GetComponent<Renderer>().sortingLayerName = "Default";
        this.GetComponent<Renderer>().sortingOrder = 4;
    }

    // Actualización
    void Update()
    {
        // Si no está atacando, incrementar el contador
        timer += Time.deltaTime;
        

        // Si el contador supera el tiempo de ataque, iniciar el ataque
        if (timer >= GameManager.instance.f_AttackDelay)
        {
            StartCoroutine(Attack());
        }

        CheckHealth();
    }

    public override void AddPoints(Model m)
    {
        m.AddPoints(GameManager.instance.f_points);
    }

    public override void Reset()
    {
        _life = GameManager.instance.f_maxLife;
    }

    public void TakeDamage(float dmg)
    {
        //SoundManager.PlaySound("enemyFlowerHitSound");
        _life -= dmg;
        //Debug.Log("Flower recibio " + dmg + " de dano");
        //Debug.Log("su vida ahora es : " + _life);
    }

    // Corrutina para el ataque
    IEnumerator Attack()
    {
        // Cambiar el estado de ataque a verdadero
        isAttacking = true;

        // Encontrar al jugador más cercano
        Transform target = FindClosestPlayer();

        // Si hay un jugador cercano, mirarlo y dispararle
        if (target != null)
        {
            // Disparar tres bolas de fuego
            ShootFireballs(target);
        }

        // Reiniciar el contador
        ResetTimer();

        yield return null;
    }

    Transform FindClosestPlayer()
    {
        Model[] targets = GameObject.FindObjectsOfType<Model>();

        float minDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Model target in targets)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);

            if (target.GetComponent<Model>())
            {
                if (target.GetComponent<Model>().isAlive && distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = target.transform;
                }
            }
            else if (distance < minDistance)
            {
                minDistance = distance;
                closestTarget = target.transform;
            }
        }

        return closestTarget;
    }

    private void CheckHealth()
    {
        if (_life <= 0)
        {
            //SoundManager.PlaySound("enemyFlowerDeath");
            ShootFireballsOnDead();
            //if (newFreezeEffect != null)
            //{
            //    newFreezeEffect.GetComponent<FreezeEffectScript>().StopFreeze();
            //}
            Debug.Log("Murio");
            EnemyFactory.Instance.ReturnEnemy(this);
        }
    }

    // Función para disparar tres bolas de fuego
    void ShootFireballs(Transform target)
    {
        // Calcular el vector dirección entre el enemigo y el jugador
        Vector2 direction = target.position - _bulletDir.position;

        // Normalizar el vector dirección
        direction.Normalize();

        // Calcular el ángulo entre el vector dirección y el eje x
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Instanciar tres bolas de fuego con una separación angular
        for (int i = -1; i <= 1; i++)
        {

            // Calcular el ángulo de la bola de fuego
            float fireballAngle = angle + i * GameManager.instance.f_fireballAngleVariation;
            // Calcular la dirección de la bola de fuego
            _bulletDir.rotation = Quaternion.Euler(0, 0, fireballAngle) * Quaternion.Euler(0, 0, 90);

            // Instanciar la bola de fuego en la posición del enemigo
            var p = ProjectileFactory.Instance.proyectilePool.GetObject();
            if (!p) return;

            p.transform.SetPositionAndRotation(_bulletDir.position, _bulletDir.rotation);
        }
    }

    void ShootFireballsOnDead()
    {
        // Instanciar bolas de fuego con una separación angular
        for (int i = -2; i <= 4; i++)
        {
            // Calcular el ángulo de la bola de fuego
            float fireballAngle = i * GameManager.instance.f_fireballAngleVariationOnDeath;
            //Debug.Log(fireballAngle);
            // Calcular la dirección de la bola de fuego
            _bulletDir.rotation = Quaternion.Euler(0, 0, fireballAngle) * Quaternion.Euler(0, 0, 90);

            // Instanciar la bola de fuego en la posición del enemigo
            var p = ProjectileFactory.Instance.proyectilePool.GetObject();
            if (!p) return;

            p.transform.SetPositionAndRotation(_bulletDir.position, _bulletDir.rotation);

            //Instantiate(enemyFireballPrefab, BulletDirFlower.position, BulletDirFlower.rotation);
        }
    }

    // Función para reiniciar el contador
    void ResetTimer()
    {
        // Poner el contador a cero
        timer = 0f;

        // Cambiar el estado de ataque a falso
        isAttacking = false;
    }

    //public void Freeze(float timeToFreeze)
    //{
    //    StartCoroutine(FreezedState(timeToFreeze));
    //}

    //IEnumerator FreezedState(float t)
    //{
    //    if (newFreezeEffect == null)
    //    {
    //        newFreezeEffect = Instantiate(freezeEffect, transform.position, Quaternion.identity);
    //    }
    //    freezeTime += t;
    //    isFreezed = true;
    //    Debug.Log("Freezed");
    //    yield return new WaitForSeconds(freezeTime);
    //    isFreezed = false;
    //    newFreezeEffect.GetComponent<FreezeEffectScript>().StopFreeze();
    //}
}