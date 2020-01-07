using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoClickDetector : MonoBehaviour
{
    public bool m_IsPressed = false;
    public Collider m_Collider;

    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRayHit()
    {
        m_IsPressed = true;
    }
}
