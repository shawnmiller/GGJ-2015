using UnityEngine;
using UnityEngine.UI;

public class LevelColors : MonoBehaviour
{
    public Color[] colors = new Color[4];

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
                    Image image = hud.transform.GetChild(i).GetComponent<Image>();
                    image.enabled = true;
                    image.color = colors[i];
                }
                else
                {
                    Image image = hud.transform.GetChild(i).GetComponent<Image>();
                    image.enabled = false;
                }
            }
        }
    }
}