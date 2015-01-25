using UnityEngine;
using UnityEngine.UI;

public class LevelColors : MonoBehaviour
{
    public static ColorType NONEXIST = ColorType.Black;

    public ColorType[] colors = new ColorType[4];

    void Start()
    {
        if (colors.Length > 4)
        {
            Debug.LogError("More than 4 colors allotted for this level.");
            Debug.Break();
        }

        GameObject hud = GameObject.Find("HUD");
        if (hud)
        {
            for (int i = 0; i < 4; ++i)
            {
                if (i < colors.Length)
                {
                    Image image = hud.transform.GetChild(i).GetChild(0).GetComponent<Image>();
                    image.enabled = true;
                    image.color = PickColor.Get(colors[i]);
                }
                else
                {
                    Image image = hud.transform.GetChild(i).GetChild(0).GetComponent<Image>();
                    image.enabled = false;
                }
            }
        }
    }

    public ColorType GetColor(int color)
    {
        if (color < 0 || color >= colors.Length)
        {
            return NONEXIST;
        }
        else
        {
            return colors[color];
        }
    }
}