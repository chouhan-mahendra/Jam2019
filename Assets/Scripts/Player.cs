using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float stealthTimeout = 3f;
    public bool stealthEnabled = true;
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
        if (velocity != Vector2.zero) 
            transform.rotation = 
                Quaternion.LookRotation(Vector3.forward, velocity);
        rb.velocity = velocity;
        
        if (Input.GetKeyDown("space") && stealthEnabled)
            StartCoroutine(startStealthMode());
        
    }
    IEnumerator startStealthMode()
    {
        stealthEnabled = false;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color color = sprite.color;
        stealth = true;
        color.a = 0.3f;
        sprite.color = color; 
        yield return new WaitForSeconds(stealthTimeout);
        color.a = 1f;
        sprite.color = color;
        yield return new WaitForSeconds(stealthTimeout);
        stealthEnabled = true;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Pickup")) {
            other.gameObject.GetComponent<Renderer>().material.color = Color.red;
            other.gameObject.tag = "Dead";
            Destroy(other.gameObject,0.5f);
        }
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
