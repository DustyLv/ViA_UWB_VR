using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SensorDataRaw
{
    public int id;
    public string address;
    public List<Datastream> datastreams;
}

[System.Serializable]
public class Datastream
{
    public string id;
    public string current_value;
    public string at;
}
