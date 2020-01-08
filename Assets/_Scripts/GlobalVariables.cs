using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public const float MAX_RAYCAST_DISTANCE = 5f;
    public AvatarItem DEFAULT_AVATAR_PREFAB;
    public Transform RIGHT_ARM;
    public Transform LEFT_ARM;

    public static GlobalVariables i;

    void Awake()
    {
        i = this;
    }

}
