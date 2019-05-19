using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [HideInInspector]
    public static GameSettings instance = null;

    public float Gravity = 9.81f;

    [Tooltip("Distance within which a character is considered grounded.")]
    public float GroundTolerance;

    void Start()
    {
        if(instance != null)
            throw new Exception("GameSettings Instance Found. Is there more than one GameSettings object?");
        instance = this;
    }

}
