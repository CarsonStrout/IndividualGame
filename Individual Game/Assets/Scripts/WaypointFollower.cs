using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    private Rigidbody2D rb;

    [SerializeField] private float speed = 4f; // can change the speed of each individual moving platform

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // changes the next waypoint when one is connected with
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        
        if (Input.GetKey(KeyCode.LeftControl)) // stops the platforms when left shift is held
        {
            rb.velocity = Vector2.zero;
        }
        else // causes the moving platform to move back and forth between selected "waypoints"
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
    }
}
