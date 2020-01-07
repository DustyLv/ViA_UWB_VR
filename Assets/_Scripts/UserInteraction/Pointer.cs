using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    [SerializeField] private float m_DefaultLength;

    [SerializeField] private GameObject m_Dot;

    public Camera m_Camera { get; private set; } = null;

    private LineRenderer m_LineRenderer = null;
    private VRInputModule m_InputModule = null;
     
    

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
        m_Camera.enabled = false;

        m_LineRenderer = GetComponent<LineRenderer>();
        m_DefaultLength = GlobalVariables.MAX_RAYCAST_DISTANCE;
    }

    private void Start()
    {
        m_InputModule = EventSystem.current.gameObject.GetComponent<VRInputModule>();
    }

    void FixedUpdate()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        // use default or distance
        PointerEventData data = m_InputModule.m_Data;
        RaycastHit hit = CreateRaycast();

        //float targetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;
        float colliderDistance = hit.distance == 0 ? m_DefaultLength : hit.distance;
        float canvasDistance = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;

        float targetLength = Mathf.Min(colliderDistance, canvasDistance);

        // raycast
        //RaycastHit hit = ExtensionMethods.CreateRaycast(transform ,targetLength);

        // default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        //or based on hit
        //if(hit.collider != null)
        //{
        //    endPosition = hit.point;
        //}

        // set position of the dot
        m_Dot.transform.position = endPosition;

        // set linerenderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);


    }
    private RaycastHit CreateRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);

        return hit;
    }
}
