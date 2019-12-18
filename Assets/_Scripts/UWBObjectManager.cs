using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UWBObjectManager : MonoBehaviour
{
    public UWBObject UWBObjectPrefab;
    public List<UWBObject> UWBObjects;
    //public GameObject[] UWBAddonObjects;

    public UWBObject activeSelectedUWBObject;

    public static UWBObjectManager i;

    private void Awake()
    {
        i = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FindAllUWBObjects()
    {
        UWBObjects.AddRange(GameObject.FindObjectsOfType<UWBObject>());
    }

    public UWBObject FindObjectByAddress(string address)
    {
        foreach (UWBObject uwbObj in UWBObjects)
        {
            if (uwbObj.UWBAddress == address) return uwbObj;
        }
        return null;
    }

    public bool SceneHasObjectWithAddress(string address)
    {
        if (UWBObjects.Count > 0)
        {
            foreach (UWBObject uwbObj in UWBObjects)
            {
                if (uwbObj.UWBAddress == address) return true;
            }
        }
        return false;
    }

    public void CreateNewUWBObject(string address)
    {
        UWBObject uwb = Instantiate(UWBObjectPrefab);
        uwb.UWBAddress = address;
        uwb.gameObject.name = address;
        UWBObjects.Add(uwb);
    }
}
