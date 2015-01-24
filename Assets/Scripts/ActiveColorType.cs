using UnityEngine;

public class ActiveColorType : MonoBehaviour
{
    [SerializeField]
    private ColorType type;
    public ColorType Type
    {
        get { return type; }
        set
        {
            type = value;
            renderer.material.color = PickColor.Get(type);
        }
    }
}