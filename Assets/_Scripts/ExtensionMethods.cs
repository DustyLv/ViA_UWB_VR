using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static Quaternion StringToQuaternion(string sQuaternion)
    {
        // split the items
        string[] sArray = sQuaternion.Split(';');

        // store as a Vector3
        Quaternion result = new Quaternion(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]),
            float.Parse(sArray[3]));

        return result;
    }

    public static RaycastHit CreateRaycast(Transform t ,float length)
    {
        RaycastHit hit;

        Ray ray = new Ray(t.position, t.forward);
        Physics.Raycast(ray, out hit, length);
        return hit;
    }

    public static RaycastHit CreateRaycast(Transform t, float length, LayerMask layerMask)
    {
        RaycastHit hit;

        Ray ray = new Ray(t.position, t.forward);
        Physics.Raycast(ray, out hit, length, layerMask);
        return hit;
    }
}
