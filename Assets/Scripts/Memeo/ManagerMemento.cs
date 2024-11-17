using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMemento : MonoBehaviour
{
    public static ManagerMemento instance;
    public List<Rewind> allRewinds;

    private void Awake()
    {
        instance = this;
    }

    //Tomo todos mis objetos que hereden de Rewind, que son los que voy a "recordar" cuando presione la tecla
    void Start()
    {
        allRewinds = new List<Rewind>();
        var listTemp = FindObjectsOfType<Rewind>();

        foreach (var item in listTemp)
        {
            allRewinds.Add(item);
        }
    }

    public void GoBack(Rewind r)
    {
        var _r = r;
        if (allRewinds.Contains(_r))
        {
            _r.remembering = true;
            _r.StartAction();
        }
    }
}
