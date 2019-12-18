using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class ActiveUWBObjectSelecter : MonoBehaviour {
    [SerializeField]
    private Transform pointerObject;

    public TMPro.TextMeshProUGUI activeTagLabel;

    private GameObject lastSelectedUWBAvatar = null;

    [SerializeField]
    private LayerMask layerMask = -1;

    public ActiveUWBObjectDataOutputUI UWBUI;

    void Start () {

        InputDeviceController.i.triggerButtonEvent.AddListener(OnSelectUWBAvatar);
    }

    // Update is called once per frame
    void FixedUpdate () {

        RaycastHit hit = ExtensionMethods.CreateRaycast(pointerObject, GlobalVariables.MAX_RAYCAST_DISTANCE, layerMask);

        if (hit.collider != null)
        {
            lastSelectedUWBAvatar = null;
            lastSelectedUWBAvatar = hit.transform.gameObject;
        }
    }

    void OnSelectUWBAvatar(bool pressed)
    {
        if (pressed)
        {
            if (lastSelectedUWBAvatar != null)
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