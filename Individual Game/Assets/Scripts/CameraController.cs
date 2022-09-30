using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, player.position.y + 1f, transform.position.z); // camera follows player on the y-axis, but stays the same on x-axis
    }
}
