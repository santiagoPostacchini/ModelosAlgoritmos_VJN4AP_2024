using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerWalksound, playerHitSound, playerDeathSound, enemySpawnSound, enemyFlowerHitSound, enemyFlowerDeathSound, enemyInvokerTeleportSound, enemyInvokerInvokeSound, enemyInvokerHitSound, enemyInvokerDeathSound, bulletSpawnSound, bulletDestroySound, soulDamageSound, soulDeathSound;
    static AudioSource audioSrc;
    
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();

        playerWalksound = Resources.Load<AudioClip>("playerWalk");
        playerHitSound = Resources.Load<AudioClip>("playerHit");
        playerDeathSound = Resources.Load<AudioClip>("");
        enemySpawnSound = Resources.Load<AudioClip>("enemySpawn");
        enemyFlowerHitSound = Resources.Load<AudioClip>("enemyFlowerHit");
        enemyFlowerDeathSound = Resources.Load<AudioClip>("enemyFlowerDeath");
        enemyInvokerTeleportSound = Resources.Load<AudioClip>("enemyInvokerTeleport");
        enemyInvokerInvokeSound = Resources.Load<AudioClip>("enemyInvokerInvoke");
        enemyInvokerHitSound = Resources.Load<AudioClip>("enemyInvokerHit");
        enemyInvokerDeathSound = Resources.Load<AudioClip>("enemyInvokerDeath");
        bulletSpawnSound = Resources.Load<AudioClip>("bulletSpawn");
        bulletDestroySound = Resources.Load<AudioClip>("bulletDestroy");
        soulDamageSound = Resources.Load<AudioClip>("");
        soulDeathSound = Resources.Load<AudioClip>("soulDeath");

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "playerWalkSound":
                audioSrc.pitch = Random.Range(0.5f, 1.5f);
                audioSrc.PlayOneShot(playerWalksound);
                break;
            case "playerHitSound":
                audioSrc.pitch = Random.Range(0.5f, 1.5f);
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "playerDeathSound":
                //audioSrc.PlayOneShot(playerDeathSound);
                break;
            case "enemyFlowerHitSound":
                audioSrc.pitch = Random.Range(0.5f, 1.5f);
                audioSrc.PlayOneShot(enemyFlowerHitSound);
                break;
            case "enemyFlowerDeathSound":
                audioSrc.pitch = Random.Range(0.5f, 1.5f);
                audioSrc.PlayOneShot(enemyFlowerDeathSound);
                break;
            case "enemyInvokerTeleportSound":
                audioSrc.pitch = 1;
                audioSrc.PlayOneShot(enemyInvokerTeleportSound);
                break;
            case "enemyInvokerInvokeSound":
                audioSrc.pitch = 1;
                audioSrc.PlayOneShot(enemyInvokerInvokeSound);
                break;
            case "enemyInvokerHitSound":
                audioSrc.pitch = Random.Range(0.5f, 1.5f);
                audioSrc.PlayOneShot(enemyInvokerHitSound);
                break;
            case "enemyInvokerDeathSound":
                audioSrc.pitch = Random.Range(0.5f, 1.5f);
                audioSrc.PlayOneShot(enemyInvokerDeathSound);
                break;
            case "bulletSpawnSound":
                audioSrc.pitch = Random.Range(0.5f, 1.5f);
                audioSrc.PlayOneShot(bulletSpawnSound);
                break;
            case "bulletDestroySound":
                audioSrc.pitch = Random.Range(0.5f, 1.5f);
                audioSrc.PlayOneShot(bulletDestroySound);
                break;
            case "soulDamageSound":
                audioSrc.pitch = 1;
                audioSrc.PlayOneShot(soulDamageSound);
                break;
            case "soulDeathSound":
                audioSrc.pitch = 1;
                audioSrc.PlayOneShot(soulDeathSound);
                break;
        }
    }
}
