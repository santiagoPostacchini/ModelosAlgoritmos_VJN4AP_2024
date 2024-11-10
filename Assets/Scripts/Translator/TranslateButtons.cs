using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateButtons : MonoBehaviour
{
    [SerializeField] private ButtonTranslate[] _textsToTranslate = new ButtonTranslate[0];

    private int actualIndex = 1;

    private void Start()
    {
        LocalizationManager.instance.language = Languages.English;
        foreach (var item in _textsToTranslate)
        {
            item.textUI.text = LocalizationManager.instance.GetTranslate(item.ID);
        }
    }

    public void TransalteBTN()
    {
        Languages actualLang = (Languages)actualIndex;
        Debug.Log($"Accedi a {actualLang}");
        LocalizationManager.instance.language = actualLang;
        foreach (var item in _textsToTranslate)
        {
            item.textUI.text = LocalizationManager.instance.GetTranslate(item.ID);
        }

        if (actualIndex == Enum.GetValues(typeof(Languages)).Length - 1)
        {
            actualIndex = 0;
        }
        else
        {
            actualIndex++;
        }
    }
}
