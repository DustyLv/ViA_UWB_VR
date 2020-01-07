using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransformController : MonoBehaviour
{
    private bool m_CanMove = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scale(float _scale)
    {
        if (UWBObjectManager.i.activeSelectedUWBObject == null)
        {
            Debug.LogWarning("There is no active selected UWB object.");
            return;
        }

        Transform activeObject = UWBObjectManager.i.activeSelectedUWBObject.gameObject.transform;
        print("active transform: " + activeObject.name);
        Vector3 newScale = new Vector3(_scale, _scale, _scale);
        activeObject.localScale = newScale;

    }
}
