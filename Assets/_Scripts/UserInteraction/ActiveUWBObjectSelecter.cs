using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using Valve.VR;

public class ActiveUWBObjectSelecter : MonoBehaviour {
    [SerializeField]
    private Transform pointerObject;

    public TMPro.TextMeshProUGUI activeTagLabel;

    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_SelectAction;

    private GameObject lastSelectedUWBAvatar = null;

    [SerializeField]
    private LayerMask layerMask = -1;

    public ActiveUWBObjectDataOutputUI UWBUI;

    void Start () {

    }

    // Update is called once per frame
    void FixedUpdate () {



        if (m_SelectAction.GetStateDown(m_TargetSource))
        {
            RaycastHit hit = ExtensionMethods.CreateRaycast(pointerObject, GlobalVariables.MAX_RAYCAST_DISTANCE, layerMask);

            if (hit.collider != null)
            {
                lastSelectedUWBAvatar = null;
                lastSelectedUWBAvatar = hit.transform.gameObject;
            }

            SetActiveUWBObject(lastSelectedUWBAvatar);
        }
    }

    public void SetActiveUWBObject(GameObject targetObject){
        // first, the raycast collides with the graphics collider,and that is a child of the graphics object 
        GameObject graphicsObjectRoot = targetObject.transform.parent.gameObject;
        // then we get the parent of the graphics object which holds the UWBObject script
        UWBObject uwbObjectRoot = graphicsObjectRoot.transform.parent.gameObject.GetComponent<UWBObject>();
        if(uwbObjectRoot != null){
            UWBObjectManager.i.activeSelectedUWBObject = uwbObjectRoot;
            activeTagLabel.text = uwbObjectRoot.gameObject.name;
            UWBUI.InitNewSelectedObject();
        }
    }
}