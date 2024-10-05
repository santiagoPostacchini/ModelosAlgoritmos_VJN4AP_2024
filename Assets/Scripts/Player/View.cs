using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public Slider lifeBar;
    public HUD hud;

    Material _myMat;
    Animator _myAnim;
    AudioSource _myAudioSource;

    void Start()
    {
        _myMat = GetComponent<Renderer>().material;
        _myAnim = GetComponent<Animator>();
        //_myAudioSource = GetComponent<AudioSource>();
    }

    public void UpdateHudLife(float value)
    {
        lifeBar.value = value;
    }

    public void UpdatePoints(int value)
    {
        hud.UpdatePoints(value);
    }

    public void DeathMaterial()
    {
        _myMat.color = Color.red; 
    }

    public void movAnimHandler(Vector2 mov)
    {
        _myAnim.SetInteger("MovX", Mathf.RoundToInt(mov.x));
        _myAnim.SetInteger("MovY", Mathf.RoundToInt(mov.y));
    }

    public void Footstep()
    {

    }

    //public void HurtAnimation()
    //{
    //    _myAnim.SetTrigger("Hurt");
    //}

    //public void HurtSound()
    //{

    //}
}
