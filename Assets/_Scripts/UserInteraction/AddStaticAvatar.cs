using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStaticAvatar : MonoBehaviour
{
    public GameObject m_AvatarPrefab;

    public Transform m_PlayerTransform;

    public float m_SpawnOffsetFromPlayer;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddAvatar()
    {
        GameObject avatar = Instantiate(m_AvatarPrefab);
        Vector3 pos = m_PlayerTransform.position + (m_PlayerTransform.forward * m_SpawnOffsetFromPlayer);
        pos.y = 0f;
        avatar.transform.position = pos;
    }
}
