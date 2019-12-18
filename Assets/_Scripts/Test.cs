using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Test : MonoBehaviour
{
    [SerializeField]
    public List<InputDevice> leftHandDevices;
    public List<InputDevice> rightHandDevices;

    //private InputDevice leftHand;

    void Start()
    {
        leftHandDevices = new List<InputDevice>();
        rightHandDevices = new List<InputDevice>();

        

        //InputDevices.GetDevicesWithRole(InputDeviceRole.LeftHanded, leftHandDevices);
        //InputDevices.GetDevicesWithRole(InputDeviceRole.RightHanded, rightHandDevices);

        //print(leftHandDevices.Count);

    }

    // Update is called once per frame
    void Update()
    {
        //InputDevices.GetDevicesWithRole(InputDeviceRole.LeftHanded, leftHandDevices);

        //Debug.Log("Left handed device count: " + leftHandDevices.Count);

        //if(leftHandDevices.Count >= 1)
        //{
        //    print("devices more than 1");
        //    float triggerValue;
        //    if(leftHandDevices[0].TryGetFeatureValue(CommonUsages.trigger, out triggerValue))
        //    {
        //        Debug.Log("Trigger value: " + triggerValue);
        //    }

        //}

        //if (leftHandDevices.Count < 1)
        //{
        //    InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
        //    //if (leftHandDevices.Count > 0)
        //    //{
        //    //    leftHand = leftHandDevices[0];
        //    //    Debug.Log("Found more than one left hand!");
        //    //}
        //}

        //if (leftHandDevices.Count == 1)
        //{
        //    UnityEngine.XR.InputDevice device = leftHandDevices[0];
        //    Debug.Log(string.Format("Device name '{0}' with role '{1}'", device.name, device.role.ToString()));

        //    bool triggerValue;
        //    bool menuButton;
        //    if(device.TryGetFeatureValue(CommonUsages.menuButton, out menuButton))
        //    {
        //        print("menu pressed " + menuButton);
        //    }
        //    if (device.TryGetFeatureValue(CommonUsages.gripButton, out triggerValue) && triggerValue)
        //    {
        //        Debug.Log("button is pressed");
        //    }
        //}
        //else if (leftHandDevices.Count > 1)
        //{
        //    Debug.Log("Found more than one left hand!");
        //}

    }

    public void PrintMessage(){
        print("Message.");
    }
}
