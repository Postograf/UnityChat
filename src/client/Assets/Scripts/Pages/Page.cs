using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour 
{
    public virtual void Enter() 
    {  
        gameObject.SetActive(true);
    }

    public virtual void Exit() 
    {
        gameObject.SetActive(false);
    }
}