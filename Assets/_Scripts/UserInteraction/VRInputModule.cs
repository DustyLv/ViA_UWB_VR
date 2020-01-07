using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.EventSystems;


public class VRInputModule : BaseInputModule
{
    [SerializeField] private Pointer m_Pointer = null;
    public PointerEventData m_Data { get; private set; } = null;

    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_ClickAction;

    //private GameObject m_CurrentObject = null;
    

    protected override void Start()
    {
        m_Data = new PointerEventData(eventSystem);
        m_Data.position = new Vector2(m_Pointer.m_Camera.pixelWidth / 2, m_Pointer.m_Camera.pixelHeight / 2);
    }

    public override void Process()
    {
        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);

        HandlePointerExitAndEnter(m_Data, m_Data.pointerCurrentRaycast.gameObject);

        ExecuteEvents.Execute(m_Data.pointerDrag, m_Data, ExecuteEvents.dragHandler);


        if (m_ClickAction.GetStateDown(m_TargetSource))
        {
            Press();
        }

        if (m_ClickAction.GetStateUp(m_TargetSource))
        {
            Release();
        }
    }

    public void Press()
    {
        m_Data.pointerPressRaycast = m_Data.pointerCurrentRaycast;

        m_Data.pointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_Data.pointerPressRaycast.gameObject);
        m_Data.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(m_Data.pointerPressRaycast.gameObject);

        ExecuteEvents.Execute(m_Data.pointerPress, m_Data, ExecuteEvents.pointerDownHandler);
        ExecuteEvents.Execute(m_Data.pointerDrag, m_Data, ExecuteEvents.beginDragHandler);
    }

    public void Release()
    {
        GameObject pointerRelease = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_Data.pointerCurrentRaycast.gameObject);

        if (m_Data.pointerPress == pointerRelease)
            ExecuteEvents.Execute(m_Data.pointerPress, m_Data, ExecuteEvents.pointerClickHandler);

        ExecuteEvents.Execute(m_Data.pointerPress, m_Data, ExecuteEvents.pointerUpHandler);

        ExecuteEvents.Execute(m_Data.pointerDrag, m_Data, ExecuteEvents.endDragHandler);

        m_Data.pointerPress = null;
        m_Data.pointerDrag = null;

        m_Data.pointerCurrentRaycast.Clear();
    }
}
