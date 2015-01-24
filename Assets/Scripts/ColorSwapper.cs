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

    void SetActiveColor(string color)
    {
        int colorNum = ParseToNum(color);
        if (levelColors && colorNum < levelColors.colors.Length)
        {
            active = levelColors.colors[colorNum];
        }
    }

    private int ParseToNum(string color)
    {
        switch (color[0])
        {
            case '0':
                return 0;
            case '1':
                return 1;
            case '2':
                return 2;
            case '3':
                return 3;
            default:
                Debug.Log("Invalid string on ColorSwapper.SetActiveColor: " + color);
                return -1;
        }
    }

    void OnLevelWasLoaded(int level)
    {
        levelColors = GameObject.FindObjectOfType<LevelColors>();
    }
}
