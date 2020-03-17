﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heli : MonoBehaviour
{
    public float speed;
    public int lives;
    public Vector3 defaultPos;
    public Text soldierText;
    public Text livesText;
    public Text scoreText;
    public int carryCount = 0;
    public int carryLimit = 3;
    public int score = 0;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2( Input.GetAxis("Horizontal") * Time.deltaTime * speed, Input.GetAxis("Vertical") * Time.deltaTime * 300));
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Obstacle":
                livesText.text = "Lives: " + --lives;
            
                transform.position = defaultPos;
                rb.velocity = Vector2.zero;
                if (lives == 0)
                {
                    //Todo
                }

                break;
            
            case "Soldier" when carryCount < carryLimit:
                carryCount++;
                Destroy(other.gameObject);
                soldierText.text = "Soldiers: " + carryCount + "/" + carryLimit;
                break;
            
            case "Hospital":
                score += carryCount;
                carryCount = 0;
                scoreText.text = "Score: " + score;
                soldierText.text = "Soldiers: " + carryCount + "/" + carryLimit;
                break;
        }
    }
    
}
