using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateButtons : MonoBehaviour
{
    public void TransalteBTN()
    {
        if (LocalizationManager.instance.actualIndex == Enum.GetValues(typeof(Languages)).Length - 1)
        {
            LocalizationManager.instance.actualIndex = 0;
        }
        else
        {
            LocalizationManager.instance.actualIndex++;
        }
        LocalizationManager.instance.TranslateButton(LocalizationManager.instance.actualIndex);
    }
}