using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject [] waypoints;
    public float speed;
    private int currentIndex = 0;
    private float radius = 1;
    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(waypoints[currentIndex].transform.position,
                    this.transform.position) < 0.1f) {
            currentIndex = (currentIndex + 1) % waypoints.Length;
        }

        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[currentIndex].transform.position,
            Time.deltaTime * speed);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, waypoints[currentIndex].transform.position - transform.position);
    }
}
