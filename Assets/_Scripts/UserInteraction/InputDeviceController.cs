using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

[System.Serializable]
public class PrimaryButtonEvent : UnityEvent<bool> { }
[System.Serializable]
public class TriggerButtonEvent : UnityEvent<bool> { }

public class InputDeviceController : MonoBehaviour
{

    public PrimaryButtonEvent primaryButtonEvent;
    public TriggerButtonEvent triggerButtonEvent;


    private bool lastButtonStatePrimary = false;
    private bool lastButtonStateTrigger = false;

    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();

    private List<InputDevice> devicesWithPrimary = new List<InputDevice>();
    private List<InputDevice> devicesWithTrigger = new List<InputDevice>();

    private List<InputDevice> allDevices = new List<InputDevice>();

    public static InputDeviceController i;

    private void Awake()
    {
        i = this;
    }

    void Start()
    {
        if (primaryButtonEvent == null)
        {
            primaryButtonEvent = new PrimaryButtonEvent();
        }
        if (triggerButtonEvent == null)
        {
            triggerButtonEvent = new TriggerButtonEvent();
        }
        InputTracking.nodeAdded += InputTracking_nodeAdded;
    }

    // check for new input devices when new XRNode is added
    private void InputTracking_nodeAdded(XRNodeState obj)
    {
        UpdateInputDevices();
    }

    // Update is called once per frame
    void Update()
    {
        //FindDevices();


        bool tempStatePrimary = false;
        bool tempStateTrigger = false;
        bool invalidDeviceFound = false;
        foreach (var device in devicesWithPrimary)
        {
            bool primaryButtonState = false;
            tempStatePrimary = device.isValid // the device is still valid
                        && device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out primaryButtonState) // did get a value
                        && primaryButtonState // the value we got
                        || tempStatePrimary; // cumulative result from other controllers
            if (!device.isValid)
                invalidDeviceFound = true;
        }

        foreach (var device in devicesWithTrigger)
        {
            bool triggerButtonState = false;
            tempStateTrigger = device.isValid // the device is still valid
                        && device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonState) // did get a value
                        && triggerButtonState // the value we got
                        || tempStateTrigger; // cumulative result from other controllers
            if (!device.isValid)
                invalidDeviceFound = true;
        }

        if (tempStatePrimary != lastButtonStatePrimary) // Button state changed since last frame
        {
            primaryButtonEvent.Invoke(tempStatePrimary);
            lastButtonStatePrimary = tempStatePrimary;
        }

        if (tempStateTrigger != lastButtonStateTrigger) // Button state changed since last frame
        {

            triggerButtonEvent.Invoke(tempStateTrigger);
            lastButtonStateTrigger = tempStateTrigger;
        }

        if (invalidDeviceFound || devicesWithPrimary.Count == 0 || devicesWithTrigger.Count == 0) // refresh device lists
            UpdateInputDevices();
    }

    void UpdateInputDevices()
    {
        devicesWithPrimary.Clear();
        UnityEngine.XR.InputDevices.GetDevices(allDevices);
        bool discardedValue;
        foreach (var device in allDevices)
        {
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out discardedValue))
            {
                devicesWithPrimary.Add(device); // Add any devices that have a primary button.
            }
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out discardedValue))
            {
                devicesWithTrigger.Add(device); // Add any devices that have a primary button.
            }
        }
    }

    void FindDevices()
    {
        //InputDevices.GetDevices(allDevices);
        InputDevices.GetDevicesWithRole(InputDeviceRole.LeftHanded, leftHandDevices);
        InputDevices.GetDevicesWithRole(InputDeviceRole.RightHanded, rightHandDevices);

    }
    
    public InputDevice GetDevice(bool getLeftHand)
    {
        //TODO: check if we actually have a device!!!!!!!!!!

        if (getLeftHand)
        {
            return leftHandDevices[0];
            //if (leftHandDevices.Count >= 1)
            //{
            //    return leftHandDevices[0];                
            //}

        }
        else
        {
            //if(rightHandDevices.Count >= 1)
            //{
                return rightHandDevices[0];

            //}
        }
    }
}
