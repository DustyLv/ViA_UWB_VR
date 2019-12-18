using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public const float MAX_RAYCAST_DISTANCE = 5f;
    public AvatarItem DEFAULT_AVATAR_PREFAB;

    public static GlobalVariables i;

    void Awake()
    {
        i = this;
    }

}
