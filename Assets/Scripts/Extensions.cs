using UnityEngine;
using System.Collections.Generic;

public static class Extensions
{
    public static void AddUnique<T>(this List<T> list, T item)
    {
        if (!list.Contains(item))
        {
            list.Add(item);
        }
    }

    public static Vector3 Clamp(this Vector3 c, Vector3 min, Vector3 max)
    {
        return new Vector3(
            Mathf.Clamp(c.x, min.x, max.x),
            Mathf.Clamp(c.y, min.y, max.y),
            Mathf.Clamp(c.z, min.z, max.z));
    }
}