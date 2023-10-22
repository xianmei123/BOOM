#define ENABLE_LOGS

using System;
using System.Diagnostics;
using Object = UnityEngine.Object;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class Logger
{
    [Conditional("ENABLE_LOGS")]
    private static void DoLog(Action<string, Object> logFunction, string prefix, Object myObj, params object[] msg)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        var name = (myObj ? myObj.name : "NullObject").Color("lightblue");
        logFunction($"[{Time.frameCount}] {prefix}[{name}]: {string.Join("; ", msg)}\n ", myObj);
#endif
    }

    [Conditional("ENABLE_LOGS")]
    public static void Log(this Object myObj, params object[] msg)
    {
        DoLog(Debug.Log, "", myObj, msg);
    }

    [Conditional("ENABLE_LOGS")]
    public static void LogError(this Object myObj, params object[] msg)
    {
        DoLog(Debug.LogError, "<!>".Color("red"), myObj, msg);
    }

    [Conditional("ENABLE_LOGS")]
    public static void LogWarning(this Object myObj, params object[] msg)
    {
        DoLog(Debug.LogWarning, "<?>".Color("yellow"), myObj, msg);
    }

    [Conditional("ENABLE_LOGS")]
    public static void LogSuccess(this Object myObj, params object[] msg)
    {
        DoLog(Debug.Log, "<$>".Color("green"), myObj, msg);
    }
}

public static class StringColorExtensions
{
    public static string Color(this string str, string color)
    {
        return $"<color={color}>{str}</color>";
    }

    public static string ColorBlue(this string str)
    {
        return $"<color=blue>{str}</color>";
    }

    public static string ColorGreen(this string str)
    {
        return $"<color=green>{str}</color>";
    }

    public static string ColorRed(this string str)
    {
        return $"<color=red>{str}</color>";
    }

    public static string ColorYellow(this string str)
    {
        return $"<color=yellow>{str}</color>";
    }
}