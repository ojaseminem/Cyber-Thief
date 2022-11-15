using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Fade : MonoBehaviour
{
    
    private void Start()
    {

        //Invoke(nameof(Move), .5f);
    }

    private void Move()
    {
        transform.DOMoveY(-10f, 4);
    }
}
