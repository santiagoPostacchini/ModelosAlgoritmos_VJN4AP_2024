using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance = default;
    public Languages language = default;

    public int actualIndex = 0;

    [SerializeField] private DataLocalization[] _data = default;

    [SerializeField] private Dictionary<Languages, Dictionary<string, string>> _translate = new();

    public ButtonTranslate[] _textsToTranslate = new ButtonTranslate[0];


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        TranslateButton(actualIndex);
        foreach (var item in LocalizationManager.instance._textsToTranslate)
        {
            item.textUI.text = LocalizationManager.instance.GetTranslate(item.ID);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        _textsToTranslate = FindObjectsOfType<ButtonTranslate>();
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
        Languages actualLang = (Languages)index;
        Debug.Log($"Accedi a {actualLang}");
        language = actualLang;
        foreach (var item in LocalizationManager.instance._textsToTranslate)
        {
            item.textUI.text = LocalizationManager.instance.GetTranslate(item.ID);
        }
    }
}