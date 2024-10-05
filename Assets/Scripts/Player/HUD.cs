using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Text[] _textList;

    private void Start()
    {
        _textList = GetComponentsInChildren<Text>();
        foreach (Text textbox in _textList)
        {
            textbox.text = "0";
        }
    }
    public void UpdatePoints(float newValue)
    {
        foreach (Text textbox in _textList)
        {
            textbox.text = newValue.ToString();
        }
    }
}
