using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametricRoom : MonoBehaviour
{
    public GameObject roomPrefab;

    public Vector3 roomSize = Vector3.one;

    public void BuildRoom()
    {
        Transform room = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity).transform;
        room.localScale = roomSize;
    }
}
