using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buoyancy : MonoBehaviour
{
    [Header("Reference to water system")]
    public waterPhysics waterPhysics; // Drag your water GameObject (with waterPhysics) here

    [Header("Wave Settings (match water wave script)")]
    public float waveHeight = 0.5f;
    public float waveFrequency = 1f;

    [Header("Float Settings")]
    public float heightOffset = 0f; // Optional: adjust to make object sit slightly above water

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (waterPhysics == null)
        {
            Debug.LogError("Missing waterPhysics reference in buoyancy script!");
        }

        // Optional: Freeze Y movement from physics (so only script controls Y)
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
    }

    void FixedUpdate()
    {
        if (waterPhysics == null) return;

        // Compute wave height at object's position (same as wave.cs)
        float waveX = Mathf.Sin(Time.time * waveFrequency + transform.position.x) * waveHeight;
        float waveZ = Mathf.Sin(Time.time * waveFrequency + transform.position.z) * waveHeight;
        float wave = (waveX + waveZ) * 0.5f; // Blend X and Z waves

        // Final water height = base water Y + wave displacement + optional offset
        float targetY = waterPhysics.transform.position.y + wave + heightOffset;

        // Set object Y position to match water surface
        Vector3 newPos = new Vector3(transform.position.x, targetY, transform.position.z);
        transform.position = newPos;

        // Optional debug
        Debug.Log($"Water Surface Y: {targetY:F2}");
    }
}
