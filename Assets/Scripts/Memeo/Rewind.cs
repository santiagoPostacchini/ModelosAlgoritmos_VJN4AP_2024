using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rewind : MonoBehaviour
{
    protected MementoState memento;

    protected float recordInterval = 0.1f;

    [SerializeField] protected float recordDuration = 5f;

    [SerializeField] protected float timeBetweenMemories = 0.1f;

    public bool remembering = false;

    /// <summary>
    /// Donde voy a tomar el recuerdo y sacar de este cada variable que necesite
    /// </summary>
    /// <param name="wrappers"></param>
    protected abstract void BeRewind(ParamsMemento wrappers);

    /// <summary>
    /// Corrutina donde voy a guardar los estados
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator StartToRec();

    public virtual void Awake()
    {
        // Calculo la cantidad máxima de recuerdos en función de recordDuration y recordInterval
        int maxMemories = Mathf.CeilToInt(recordDuration / recordInterval);

        memento = new MementoState(maxMemories);

        StartCoroutine(StartToRec());
    }

    /// <summary>
    /// Pregunto si tengo recuerdos y en caso de tener tomo el ultimo llamando a la funcion que seteo el hijo
    /// Y pasando por parametro el ultimo estado a recordar que trae mi MementoState
    /// </summary>
    public IEnumerator Action()
    {
        int watchdog = 0;
        while (memento.MemoriesQuantity() > 0 && watchdog < 500)
        {
            watchdog++;
            BeRewind(memento.Remember());
            Debug.Log(Time.time);
            yield return new WaitForSeconds(timeBetweenMemories);
        }
        remembering = false;
        yield return null;
    }

    public void StartAction()
    {
        StartCoroutine(Action());
    }
}
