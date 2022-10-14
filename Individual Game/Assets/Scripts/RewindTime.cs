using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTime : MonoBehaviour
{
    private bool isRewinding = false;

    List<Vector3> positions;

    public float recordTime = 5f;

    void Start()
    {
        positions = new List<Vector3>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            StopRewind();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Rewind()
    {
        if(positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if (positions.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            positions.RemoveAt(positions.Count - 1);
        }
        positions.Insert(0, transform.position);
    }

    public void StartRewind()
    {
        isRewinding = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
    }
}
