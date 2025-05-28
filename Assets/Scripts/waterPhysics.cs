using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPhysics : MonoBehaviour
{
    // Speed at which the water rises
    [SerializeField] private float riseSpeed = 0.5f;

    // Maximum height the water can reach
    [SerializeField] private float maxHeight = 10f;

    private Vector3 startPosition;

    void Start()
    {
        // Record the starting position of the water
        startPosition = transform.position;
    }

    void Update()
    {
        // Only rise if the water is below the maximum height
        if (transform.position.y < maxHeight)
        {
            // Move the water upwards over time
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
    }

    public void ResetWater()
    {
        // Reset the water to its original position
        transform.position = startPosition;
    }
}
