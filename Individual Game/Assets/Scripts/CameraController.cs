using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] public bool horizontal;
    [SerializeField] public bool vertical;

    private void Update()
    {
        if (vertical && !horizontal)
        {
            transform.position = new Vector3(transform.position.x, player.position.y + 1f, transform.position.z); // camera follows player on the y-axis, but stays the same on x-axis
        }
        else if (horizontal && !vertical)
        {
            transform.position = new Vector3(player.position.x + 6f, transform.position.y, transform.position.z); // camera follows player on the x-axis, but stays the same on y-axis
        }
        else if (horizontal && vertical)
        {
            transform.position = new Vector3(player.position.x + 2f, player.position.y + 1f, transform.position.z); // camera follows player on both axis
        }
    }
}
