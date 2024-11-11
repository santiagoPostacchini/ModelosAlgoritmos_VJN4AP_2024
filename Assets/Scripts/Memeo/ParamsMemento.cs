using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamsMemento
{
    public object[] parameters;
    public float timestamp;

    public ParamsMemento(float time, params object[] parameterWrapper)
    {
        parameters = new object[parameterWrapper.Length];
        timestamp = time;

        for (int i = 0; i < parameterWrapper.Length; i++)
        {
            parameters[i] = parameterWrapper[i];
        }
    }
}
