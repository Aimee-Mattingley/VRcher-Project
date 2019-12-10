using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public VRInputMModule m_InputModule;

    private LineRenderer m_LineRenderer = null; 

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLine();
    }
    private void UpdateLine()
    {
        // Use default or distance
        float targetlenght = m_DefaultLength;

        // Raycast
        RaycastHit hit = CreateRaycast(targetlenght);

        // Default
        Vector3 endPosition = transform.position + (transform.forward * targetlenght);

        // Or based on hit
        if (hit.collider != null)
            endPosition = hit.point;

        // Set posistion of the dot
        m_Dot.transform.position = endPosition;

        // Set linerenderer 
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);


    }
    private RaycastHit CreateRaycast(float lenght)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);

        return hit;
    }
}
