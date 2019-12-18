using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONWorker : MonoBehaviour
{
    public List<SensorDataRaw> sensorDataRaw;
    public SensorDataRaw currentSensorDataRaw;

    private UWBObjectManager UWBMan;

    private void Start()
    {
        UWBMan = UWBObjectManager.i;
    }

    public void JsonToObject(string json)
    {
        SensorDataRaw d = JsonUtility.FromJson<SensorDataRaw>(json);
        sensorDataRaw.Add(d);
        currentSensorDataRaw = d;
        ProcessCurrentData(d);
    }

    public SensorDataRaw JsonToObjectReturn(string json)
    {
        SensorDataRaw d = JsonUtility.FromJson<SensorDataRaw>(json);
        return d;
    }

    private void Update()
    {
        
    }

    void ProcessCurrentData(SensorDataRaw currentData)
    {
        // check if there is an object with address
        if (!UWBMan.SceneHasObjectWithAddress(currentData.address))
        {
            UWBMan.CreateNewUWBObject(currentData.address);
        }
        else
        {
            SendDataToUWBObject(currentData);
        }
        currentSensorDataRaw = null;
        sensorDataRaw.RemoveAt(0);
    }

    public void SendDataToUWBObject(SensorDataRaw data)
    {
        UWBObject obj = UWBMan.FindObjectByAddress(data.address);
        if(obj != null)
            obj.ReceiveData(data);
    }

    public bool HasData()
    {
        return sensorDataRaw.Count > 0;
    }
}

