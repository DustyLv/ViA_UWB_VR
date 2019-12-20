using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRTeleport : MonoBehaviour
{
    public LayerMask layerMask = -1;
    public Transform rightHand;
    public Transform playerObject;

    private bool validTeleportAreaFound = false;
    private RaycastHit m_hit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        validTeleportAreaFound = Physics.Raycast(rightHand.position, rightHand.TransformDirection(Vector3.forward), out m_hit, Mathf.Infinity, layerMask);

    }

    public void Teleport(bool pressed)
    {
        if (validTeleportAreaFound && pressed)
        {
            Vector3 newPos = m_hit.point;
            playerObject.position = newPos;
        }
    }
}
