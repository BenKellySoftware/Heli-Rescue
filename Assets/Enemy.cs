﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =  new Vector3(Mathf.Lerp(transform.position.x, player.position.x, 0.02f),-4.5f,0);
    }
}