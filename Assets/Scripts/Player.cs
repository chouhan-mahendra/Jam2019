using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public Joystick joystick;
    private Rigidbody2D rb;
    private Boolean stealth = false;
    private GameController gameController;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else Debug.Log("Unable to find game contoller");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = new Vector2(Input.GetAxis("Horizontal") * speed,
                                Input.GetAxis("Vertical") * speed);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, velocity);
        rb.velocity = velocity;
        
        if (Input.GetKeyDown("space") && !stealth) {
            Debug.Log("Toggle Stealth!");
            toggleStealthMode();
            Invoke("toggleStealthMode",2);
        }
    }

    void toggleStealthMode() {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color color = sprite.color;
        stealth = !stealth;
        color.a = stealth ? 0.3f : 1f;
        sprite.color = color; 
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(!stealth) {
            Debug.Log("OnTrigger");
            GetComponent<AudioSource>().Play();
        }        
    }
    void OnTriggerStay2D() {
        if(!stealth) {
            Debug.Log("OnStay");
        }        
    }
}
