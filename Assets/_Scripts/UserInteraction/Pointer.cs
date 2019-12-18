using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    private float m_DefaultLength;

    public GameObject m_Dot;
    public VRInputModule m_InputModule;
    public LayerMask layerMask = -1;

    private LineRenderer m_LineRenderer = null;

    

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_DefaultLength = GlobalVariables.MAX_RAYCAST_DISTANCE;


    }

    void FixedUpdate()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        // use default or distance
        PointerEventData data = m_InputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;

        // raycast
        RaycastHit hit = ExtensionMethods.CreateRaycast(transform ,targetLength);

        // default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        //or based on hit
        if(hit.collider != null)
        {
            endPosition = hit.point;
        }

        // set position of the dot
        m_Dot.transform.position = endPosition;

        // set linerenderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);


    }
}
