using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;
    public Languages language = default;

    public int actualIndex = 0;
    [SerializeField] private DataLocalization[] _data;

    private Dictionary<Languages, Dictionary<string, string>> _translate;
    public ButtonTranslate[] _textsToTranslate = new ButtonTranslate[0];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            _translate = LanguageU.LoadTranslate(_data);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "MainMenu")
        {
            _textsToTranslate = FindObjectsOfType<ButtonTranslate>();
        }
        Debug.Log("Scene Loaded: " + scene.name);
        TranslateButton(actualIndex);
    }

    public string GetTranslate(string ID)
    {
        if (!_translate.ContainsKey(language))
            return "No lang";

        if (!_translate[language].ContainsKey(ID))
            return "No ID";

        return _translate[language][ID];
    }

    public void TranslateButton(int index)
    {
        language = (Languages)index;
        foreach (var item in _textsToTranslate)
        {
            item.textUI.text = GetTranslate(item.ID);
        }
    }
}