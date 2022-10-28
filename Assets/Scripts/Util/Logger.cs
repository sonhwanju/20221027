using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logger
{
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(string str)
    {
        Debug.Log(str);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(string str)
    {
        Debug.LogError(str);
    }
}
