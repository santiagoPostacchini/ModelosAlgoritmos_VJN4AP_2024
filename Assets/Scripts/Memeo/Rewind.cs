using System.Collections;
using UnityEngine;

public abstract class Rewind : MonoBehaviour
{
    protected MementoState memento;

    protected float recordInterval = 0.1f;

    [SerializeField] protected float recordDuration = 5f;

    [SerializeField] protected float timeBetweenMemories = 0.2f;

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
        int maxMemories = Mathf.CeilToInt(recordDuration / recordInterval);
        memento = new MementoState(maxMemories);

        StartCoroutine(StartToRec());
    }

    public void Action()
    {
        if (!remembering)
        {
            Debug.Log("Inicio rewind");
            StartCoroutine(RewindCoroutine());
        }
        else
        {
            Debug.LogWarning("Ya está en rewind");
        }
    }

    /// <summary>
    /// Pregunto si tengo recuerdos y en caso de tener tomo el ultimo llamando a la funcion que seteo el hijo
    /// Y pasando por parametro el ultimo estado a recordar que trae mi MementoState
    /// </summary>
    
    private IEnumerator RewindCoroutine()
    {
        remembering = true;

        while (memento.MemoriesQuantity() > 0)
        {
            if (memento.Remember() != null)
            {
                BeRewind(memento.Remember());
                yield return new WaitForSeconds(timeBetweenMemories);
            }
        }

        remembering = false;
    }
}
