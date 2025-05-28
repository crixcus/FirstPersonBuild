using UnityEngine;

public class FloodRise : MonoBehaviour
{
    public float riseSpeed = 0.5f; // Speed of flood rising
    public float maxHeight = 10f; // Maximum height of flood

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Move upward over time
        if (transform.position.y < maxHeight)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
    }
}
