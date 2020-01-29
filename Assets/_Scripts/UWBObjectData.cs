using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UWBObjectData
{
    public string UWBAddress;

    public Vector3 m_OverridePosition = Vector3.zero;
    public bool[] m_OverridePositionState = new bool[3]; // XYZ

    public bool allowRotation = false;

    public bool[] m_InvertPositionState = new bool[3]; // XYZ

    public Vector3 m_OffsetPosition = Vector3.zero;

    public Vector3 position = Vector3.zero;

}
