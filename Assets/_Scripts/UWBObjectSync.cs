using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWBServer_Client;

public class UWBObjectSync : MonoBehaviour
{
    public static UWBObjectSync instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SendUpdateToServer(UWBObject _senderObj)
    {
        // UWBServer_Client.ClientSend.UWBObjectData(UWBObjectSerializer.instance.Serialize(UWBObjectManager.i.activeSelectedUWBObject))

        UWBObjectData data = new UWBObjectData();

        data.UWBAddress = _senderObj.UWBAddress;
        data.m_OverridePosition = _senderObj.m_OverridePosition;
        data.m_OverridePositionState = _senderObj.m_OverridePositionState;
        data.allowRotation = _senderObj.allowRotation;
        data.m_InvertPositionState = _senderObj.m_InvertPositionState;
        data.m_OffsetPosition = _senderObj.m_OffsetPosition;

        //data.position = new Vector3(Mathf.Abs(_senderObj.transform.position.x), Mathf.Abs(_senderObj.transform.position.y), Mathf.Abs(_senderObj.transform.position.z));
        data.position = _senderObj.position;

        print("Sending updated data to server");
        ClientSend.UWBObjectData(UWBObjectSerializer.instance.Serialize(data));
    }

    public void UpdateSettingsFromServer(UWBObject _target, UWBObjectData _newSettings)
    {
        _target.m_OverridePosition = _newSettings.m_OverridePosition;
        _target.m_OverridePositionState = _newSettings.m_OverridePositionState;
        _target.allowRotation = _newSettings.allowRotation;
        _target.m_InvertPositionState = _newSettings.m_InvertPositionState;
        _target.m_OffsetPosition = _newSettings.m_OffsetPosition;
        _target.position = _newSettings.position;

        _target.UpdateObjectPosition(_target.position);

        // TEST:
        if(UWBObjectManager.i.activeSelectedUWBObject.UWBAddress == _target.UWBAddress)
        {
            GameObject.FindObjectOfType<ActiveUWBObjectDataOutputUI>().InitNewSelectedObject();
        }
    }
}
