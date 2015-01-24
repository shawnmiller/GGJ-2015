using UnityEngine;

[System.Serializable]
public struct ProjectileInfo
{
    public Vector2 direction;
    public float speed;
    public Vector3 scale;
    public ColorType color;
    public PathType path;
    [Tooltip("Center offset only works with non-linear paths.")]
    public float centerOffset;
    [Tooltip("Must be a value between 0 and 1")]
    public float startPoint;
    public bool destroyOnCollide;
    public bool permanent;
}