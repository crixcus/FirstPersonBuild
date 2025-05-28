using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    bool leverDown = true;
    Animator _anim;
    public UnityEvent onUp, onDown;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        leverDown = true;
    }

    public void ToggleLever()
    {
        if (leverDown)
        {
            _anim.SetTrigger("pDown");
            onDown.Invoke();
        }
        else
        {
            _anim.SetTrigger("pUp");
            onUp.Invoke();
        }

        leverDown = !leverDown;
    }

}
