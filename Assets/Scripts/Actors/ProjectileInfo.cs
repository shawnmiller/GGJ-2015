using UnityEngine;

[System.Serializable]
public class ProjectileInfo
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
    public bool reversed;

    public ProjectileInfo Clone()
    {
        ProjectileInfo n = new ProjectileInfo();
        n.direction = direction;
        n.speed = speed;
        n.scale = scale;
        n.color = color;
        n.path = path;
        n.centerOffset = centerOffset;
        n.startPoint = startPoint;
        n.destroyOnCollide = destroyOnCollide;
        n.permanent = permanent;
        n.reversed = reversed;

        return n;
    }
}