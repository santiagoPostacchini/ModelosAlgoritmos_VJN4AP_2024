using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public Image lifeBar;

    Material _myMat;
    //Animator _myAnim;

    void Start()
    {
        _myMat = GetComponent<Renderer>().material;
        //_myAnim = GetComponent<Animator>();
    }

    public void UpdateHudLife(float value)
    {
        lifeBar.fillAmount = value;
    }

    public void DeathMaterial()
    {
        _myMat.color = Color.red;
    }

    //public void HurtAnimation()
    //{
    //    _myAnim.SetTrigger("Hurt");
    //}
}
