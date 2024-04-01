using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    [SerializeField] private float maxPosition;
    // [SerializeField] private float newXPosition = 0f;
    private bool repositioned;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        // Debug.Log(startPosition);
    }

    void Update()
    {
        if (transform.position.x < maxPosition)
        {
            repositioned = true;
            transform.localPosition = startPosition;
        }
        else
        {
            repositioned = false;
        }

    }

    public bool GetRepositioned()
    {
        return repositioned;
    }
}
