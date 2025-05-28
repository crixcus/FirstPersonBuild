using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour
{
    public float waveHeight = 0.05f; // Lowered for subtle effect
    public float waveSpeed = 1f;
    public int rippleCount = 20; // How many ripples are added

    private Vector3[] baseVertices;
    private Vector3[] displacedVertices;
    private Mesh mesh;

    private class Ripple
    {
        public Vector3 origin;
        public float offset;

        public Ripple(Vector3 origin, float offset)
        {
            this.origin = origin;
            this.offset = offset;
        }
    }

    private List<Ripple> ripples = new List<Ripple>();

    void Start()
    {
        mesh = Instantiate(GetComponent<MeshFilter>().mesh);
        GetComponent<MeshFilter>().mesh = mesh;
        baseVertices = mesh.vertices;
        displacedVertices = new Vector3[baseVertices.Length];

        // Generate ripples at start
        for (int i = 0; i < rippleCount; i++)
        {
            Vector3 randomVertex = baseVertices[Random.Range(0, baseVertices.Length)];
            Vector3 worldPoint = transform.TransformPoint(randomVertex);
            float timeOffset = Random.Range(0f, Mathf.PI * 2f); // randomize ripple phase
            ripples.Add(new Ripple(worldPoint, timeOffset));
        }
    }

    void Update()
    {
        baseVertices.CopyTo(displacedVertices, 0);

        for (int i = 0; i < displacedVertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(baseVertices[i]);
            float yOffset = 0f;

            foreach (Ripple ripple in ripples)
            {
                float distance = Vector3.Distance(worldPos, ripple.origin);
                yOffset += Mathf.Sin(distance * 4f - Time.time * waveSpeed + ripple.offset);
            }

            yOffset /= ripples.Count; // average it to prevent wild distortion
            displacedVertices[i].y += yOffset * waveHeight;
        }

        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();
    }
}
