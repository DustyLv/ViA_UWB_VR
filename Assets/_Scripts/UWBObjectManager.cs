using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UWBObjectManager : MonoBehaviour
{
    public UWBObject UWBObjectPrefab;
    public List<UWBObject> UWBObjects; // list that stores all UWB objects in the scene

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

    /// <summary>
    /// Finds any and all UWBObject objects in the scene. Adds them to the UWBObjects list.
    /// </summary>
    void FindAllUWBObjects()
    {
        UWBObjects.AddRange(GameObject.FindObjectsOfType<UWBObject>());
    }

    /// <summary>
    /// Goes over the UWB
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public UWBObject FindObjectByAddress(string address)
    {
        foreach (UWBObject uwbObj in UWBObjects)
        {
            if (uwbObj.UWBAddress == address) return uwbObj;
        }
        return null;
    }

    /// <summary>
    /// Checks if there is an object in the scene with an address.
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Creates a new UWB objecy in the scene with address from the tag.
    /// </summary>
    /// <param name="address"></param>
    public void CreateNewUWBObject(string address)
    {
        UWBObject uwb = Instantiate(UWBObjectPrefab);
        uwb.UWBAddress = address;
        uwb.gameObject.name = address;
        UWBObjects.Add(uwb);
    }
}
