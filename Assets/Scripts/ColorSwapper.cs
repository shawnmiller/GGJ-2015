using UnityEngine;
using System.Collections;

public class ColorSwapper : MSingleton<ColorSwapper>
{
    private LevelColors levelColors;
    private Color active;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetActiveColor(int color)
    {
        if (levelColors && color < levelColors.colors.Length)
        {
            active = levelColors.colors[color];
        }
    }

    void OnLevelWasLoaded(int level)
    {
        levelColors = GameObject.FindObjectOfType<LevelColors>();
    }
}
