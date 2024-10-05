using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInvoker : Enemy, IDamage
{
    //public GameObject SpawnEffect;
    //public FreezeEffectScript freezeEffect;
    //FreezeEffectScript newFreezeEffect;
    
    private float _life;
    private float timerSpawn;
    private float timerTeleport;
    private int _invokingFlowerChance;
    //public Animator animator;
    private bool isFreezed;
    //private float _freezeTime;
    [SerializeField] private Enemy enemyToSpawn;
    

    private void Start()
    {
        isFreezed = false;
        //_freezeTime = 0;
        _life = GameManager.instance.invok_maxLife;
        timerSpawn = 0f;
        timerTeleport = 0f;
        //SpawnAnim();
        //Invoke(nameof(IdleAnim), 0.5f);
        this.GetComponent<Renderer>().sortingLayerName = "Default";
        this.GetComponent<Renderer>().sortingOrder = 4;
    }

    private void Update()
    {
        if (!isFreezed)
        {
            timerSpawn += Time.deltaTime;
            timerTeleport += Time.deltaTime;

            if (timerSpawn >= GameManager.instance.invok_spawnInterval)
            {
                _invokingFlowerChance = Random.Range(0, 101);
                if (_invokingFlowerChance < GameManager.instance.invok_invokingChance)
                {
                    SpawnEnemy();
                    //SoundManager.PlaySound("enemyInvokerInvokeSound");
                    //InvokeAnim(); //animacion de invocar
                    //Invoke(nameof(SpawnEnemy), 0.2f); //invoco
                    //Invoke(nameof(IdleAnim), 0.6f); //animacion idle
                }
                timerSpawn = 0f; // reinicio el timer
            }
            else if (timerTeleport >= GameManager.instance.invok_teleportInterval)
            {
                TeleportRandomly();
                //SoundManager.PlaySound("enemyInvokerTeleportSound");
                //TeleportAnim(); //animacion de teleport
                //Invoke(nameof(TeleportRandomly), 0.7f); //teleportarse
                //Invoke(nameof(SpawnAnim), 0.6f); //animacion de aparicion
                //Invoke(nameof(IdleAnim), 0.1f); //animacion de idle
                timerTeleport = 0f; // reinicio el timer
            }
        }

        CheckHealth();
    }

    public override void AddPoints(Model m)
    {
        m.AddPoints(GameManager.instance.invok_points);
    }

    public override void Reset()
    {
        _life = GameManager.instance.invok_maxLife;
    }

    private void CheckHealth() //revisa la vida del Invoker
    {
        if (_life <= 0)
        {
            //SoundManager.PlaySound("enemyInvokerDeathSound");
            Debug.Log("Murio");
            //if(newFreezeEffect != null)
            //{
            //    newFreezeEffect.GetComponent<FreezeEffectScript>().StopFreeze();
            //}
            EnemyFactory.Instance.ReturnEnemy(this);
        }
    }

    private void SpawnEnemy() //invoca un enemigo en una posicion aleatoria en el mapa
    {
        Vector3 enemyPosition = new Vector2(Random.Range(-9f, 9f), Random.Range(-4f, 4f));

        var e = EnemyFactory.Instance.GetEnemy(enemyToSpawn);
        if (!e) return;

        e.transform.SetPositionAndRotation(enemyPosition, Quaternion.identity);

        //Instantiate(SpawnEffect, new Vector2(flowerSpawnPosition.x, flowerSpawnPosition.y - 0.5f), Quaternion.identity);
        Debug.Log("Spawnee una flor");
    }

    private void TeleportRandomly()
    {
        //Generar posicion random en el mapa
        Vector2 randomPosition = new(
            Random.Range(-9f, 9f),
            Random.Range(-4f, 4f)
        );

        // Teletransporta el enemigo a un lugar aleatorio
        transform.position = randomPosition;
    }

    //public void Freeze(float timeToFreeze)
    //{
    //    Debug.Log("Start Freezing");
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

    //public void SpawnAnim() // setea variables del control de animacion para la animacion de aparicion
    //{
    //    animator.SetBool("Teleporting", false);
    //    animator.SetBool("Invoking", false);
    //    animator.SetBool("Idling", false);
    //    animator.SetBool("Appeared", true);
    //}
    //public void TeleportAnim() // setea variables del control de animacion para la animacion de TP
    //{
    //    animator.SetBool("Appeared", false);
    //    animator.SetBool("Invoking", false);
    //    animator.SetBool("Idling", false);
    //    animator.SetBool("Teleporting", true);
    //}
    //public void InvokeAnim() // setea variables del control de animacion para la animacion de invocacion
    //{
    //    animator.SetBool("Appeared", false);
    //    animator.SetBool("Teleporting", false);
    //    animator.SetBool("Idling", false);
    //    animator.SetBool("Invoking", true);
    //}
    //public void IdleAnim() // setea variables del control de animacion para la animacion de idle
    //{
    //    animator.SetBool("Teleporting", false);
    //    animator.SetBool("Appeared", false);
    //    animator.SetBool("Invoking", false);
    //    animator.SetBool("Idling", true);
    //}

    public void TakeDamage(float dmg)
    {
        _life -= dmg;
    }
}
