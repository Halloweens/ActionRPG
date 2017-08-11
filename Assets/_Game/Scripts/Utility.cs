using UnityEngine;
using System;
using System.Collections;

public sealed class Ref<T>
{
    private Func<T> getter;
    private Action<T> setter;

    public Ref(Func<T> getter, Action<T> setter)
    {
        this.getter = getter;
        this.setter = setter;
    }

    public T Value
    {
        get { return getter(); }
        set { setter(value); }
    }
}

public static class Utility
{
    public static int ParseToInt(string text)
    {
        return Int32.Parse(text);
    }
}

public static class Math
{
    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot;
        dir = Quaternion.Euler(angles) * dir;
        point = dir + pivot;
        return point;
    }
}