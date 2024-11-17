using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MementoState
{
    List<ParamsMemento> _rememberParams; //Lista que simula ser un stack, donde voy a agregar estados opacos
    private int _maxMemories;            //y tomarlos (del ultimo al primero)

    public MementoState(int maxMemories)
    {
        _rememberParams = new List<ParamsMemento>();
        _maxMemories = maxMemories;
    }

    /// <summary>
    /// Devuelvo la cantidad de estados (recuerdos) que tiene mi lista
    /// </summary>
    /// <returns></returns>
    public int MemoriesQuantity()
    {
        return _rememberParams.Count;
    }

    /// <summary>
    /// Tomo un recuerdo de la lista (el ultimo en este caso), lo borro y lo devuelvo
    /// </summary>
    /// <returns></returns>
    public ParamsMemento Remember()
    {
        if (_rememberParams.Count == 0)
        {
            Debug.LogWarning("No memories left to retrieve!");
            return null;
        }

        int index = _rememberParams.Count - 1;

        var currentParam = _rememberParams[index];

        _rememberParams.RemoveAt(index);

        Debug.Log($"Memory removed. Remaining memories: {_rememberParams.Count}");

        return currentParam;
    }

    /// <summary>
    /// Guardo un recuerdo en mi lista usando ParametersMemento como intermediario para que
    /// me adapte el params object a object y borro el ultimo recuerdo cuando este se excede del limite de tiempo.
    /// </summary>
    /// <param name="parameterWrapper"></param>

    public void Rec(params object[] parameterWrapper)
    {
        if (_rememberParams.Count >= _maxMemories)
        {
            _rememberParams.RemoveAt(0); // Borra el recuerdo más antiguo
        }

        float currentTime = Time.time;
        _rememberParams.Add(new ParamsMemento(currentTime, parameterWrapper));
    }
}
