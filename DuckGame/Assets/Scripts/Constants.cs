using System.Collections;
using System.Collections.Generic;
using UnityEngine;

﻿public static class Constants
{
    public const float WaterLevel = -3.0f;

    public static float WaterLevelAt (float x)
    {
        return Mathf.Sin(x);
    }
}
