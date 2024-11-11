using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    //acá las variables que necesitan los proyectiles y enemigos
    public static GameManager instance;


    [Header("Proyectiles")]
    public float p_speed = 7f, p_maxDistance = 9f;

    [Header("Enemies")]

    [Header("Enemy Spawner")]
    public float eSpwner_spawnRadius = 2f;
    public int eSpwner_spawnChance = 30;
    public readonly bool eSpwner_canSpawn = true;

    [Header("Enemy Flower")]
    public float f_AttackDelay = 2f; // Tiempo entre ataques
    public float f_fireballAngleVariation = 5f; // Ángulo entre bolas de fuego
    public float f_fireballAngleVariationOnDeath = 60f; // Ángulo entre bolas de fuego
    public int f_maxLife = 40;
    public int f_points = 10;

    [Header("Enemy Invoker")]
    public Enemy flowerEnemy;
    public int invok_points = 20;
    public int invok_maxLife = 60;
    public int invok_invokingChance = 50;
    public float invok_spawnInterval = 5f;
    public float invok_teleportInterval = 8f;

    [Header("HUD")]
    public GameObject losePanel;
    public GameObject hudPanel;
    public GameObject winPanel;
    public Text time;
    public Text timeRED;
    public Text timeBLACK;
    public bool isPlayer1Alive = true;
    public bool isPlayer2Alive = true;
    public bool isSoulAlive = true;
    public float timer;
    public int player1Points;
    public int player2Points;

    private void Awake()
    {
        instance = this;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        hudPanel.SetActive(true);
        Time.timeScale = 1;
        timer = 180f;
        if(LocalizationManager.instance != null)
            LocalizationManager.instance.OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        TimerUpdate();

        if(timer <= 0f)
        {
            WinPanel();
        }
        //if (!isSoulAlive)
        //{
        //    losePanel.SetActive(true);
        //    SetFinalScores(losePanel);
        //    hudPanel.SetActive(false);
        //    Time.timeScale = 0;
        //}
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void SetFinalScores(GameObject panel)
    {
        Text[] scoreTexts = panel.GetComponentsInChildren<Text>();
        scoreTexts[0].text = player1Points + " " + LocalizationManager.instance.GetTranslate("HUDPTSPL1");
        scoreTexts[1].text = player2Points + " " + LocalizationManager.instance.GetTranslate("HUDPTSPL2");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene()).buildIndex);
    }

    public void TimerUpdate()
    {
        int timeToDisplay = Mathf.RoundToInt(timer);
        time.text = timeToDisplay.ToString();
        timeRED.text = timeToDisplay.ToString();
        timeBLACK.text = timeToDisplay.ToString();
    }

    public void PlayerDies(Model m)
    {
        switch (m.tag)
        {
            case "PlayerOne":
                player1Points = m.GetActualPoints();
                isPlayer1Alive = false;
                break;

            case "PlayerTwo":
                player2Points = m.GetActualPoints();
                isPlayer2Alive = false;
                break;
        }

        if (!isPlayer1Alive && !isPlayer2Alive)
        {
            DeathPanel();
        }
    }

    private void DeathPanel()
    {
        losePanel.SetActive(true);
        SetFinalScores(losePanel);
        hudPanel.SetActive(false);
        Time.timeScale = 0;
    }
    
    private void WinPanel()
    {
        winPanel.SetActive(true);
        SetFinalScores(winPanel);
        hudPanel.SetActive(false);
        Time.timeScale = 0;
    }
}
