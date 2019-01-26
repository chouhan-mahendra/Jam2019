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
    private GameController gameController;
    void Start()
    {
        dir = DIR.LEFT;
        rb = GetComponent<Rigidbody2D>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else Debug.Log("Unable to find game contoller");
    }

    enum DIR { UP, LEFT, DOWN, RIGHT }
    DIR dir;

    DIR getDirection() {
        float moveHorizontal = //joystick.Horizontal + 
                Input.GetAxis ("Horizontal");
        float moveVertical = //joystick.Vertical + 
                Input.GetAxis ("Vertical");
        DIR currentDir = dir;
        if(moveHorizontal < 0)
           currentDir = DIR.LEFT;
        else if(moveHorizontal > 0)
            currentDir = DIR.RIGHT;
        else if(moveVertical < 0)
            currentDir = DIR.DOWN;
        else if(moveVertical > 0)
            currentDir = DIR.UP;
        return currentDir;    
    }

    // Update is called once per frame
    void Update()
    {
        dir = getDirection();
        Vector2 velocity = new Vector2 (0, 0);
        switch(dir) {
            case DIR.UP:
                velocity.y = speed;
            break;
            case DIR.LEFT:
                velocity.x = -speed;
            break;
            case DIR.DOWN:
                velocity.y = -speed;
            break;
            case DIR.RIGHT:
                velocity.x = speed;
            break;
        }

        velocity = new Vector2(Input.GetAxis("Horizontal") * speed,
                                Input.GetAxis("Vertical") * speed);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, velocity);

        rb.velocity = velocity;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("OnTrigger");
        GetComponent<AudioSource>().Play();
    }
}
