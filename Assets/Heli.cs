using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heli : MonoBehaviour
{
    public float speed;
    public int lives;
    public Vector3 defaultPos;

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
        Debug.Log("Hit");
        if (other.tag != "Obstacle") return;
        {
            lives--;
            transform.position = defaultPos;
            rb.velocity = Vector2.zero;
        }
    }
    
}
