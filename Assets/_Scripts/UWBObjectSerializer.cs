using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UWBObjectSerializer : MonoBehaviour
{
    public static UWBObjectSerializer instance;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Serialize(UWBObjectData _obj)
    {
        string json = JsonUtility.ToJson(_obj);

        return json;
    }

    public UWBObjectData Deserialize(string _json)
    {
        UWBObjectData uwb = new UWBObjectData();
        uwb = JsonUtility.FromJson<UWBObjectData>(_json);

        return uwb;
    }
}
