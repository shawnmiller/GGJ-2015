using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XInputDotNetPure;

public class ColorSwapper : MSingleton<ColorSwapper>
{
    private static Color DISABLED = new Color(1f, 1f, 1f, 0f);
    private static Color HIGHLIGHT = new Color(1f, 1f, 1f, 0.2f);
    private LevelColors levelColors;
    private Color activeColor;
    private Transform hud;

    private int activeIndex;

    private KeyWatcher lBumper;         // 0
    private KeyWatcher rBumper;         // 1
    private AxisKeyWatcher lTrigger;    // 2
    private AxisKeyWatcher rTrigger;    // 3
    

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        activeIndex = 0;

        hud = GameManager.Instance.hud.transform;

        lBumper = new KeyWatcher();
        rBumper = new KeyWatcher();
        lTrigger = new AxisKeyWatcher();
        rTrigger = new AxisKeyWatcher();
    }

    void Update()
    {
        GamePadState state = GamePad.GetState(PlayerIndex.One);
        lBumper.Update(state.Buttons.LeftShoulder);
        rBumper.Update(state.Buttons.RightShoulder);
        lTrigger.Update(state.Triggers.Left);
        rTrigger.Update(state.Triggers.Right);

        if (lBumper.Down()) { SetActiveColor(0); }
        if (rBumper.Down()) { SetActiveColor(1); }
        if (lTrigger.Down()) { SetActiveColor(2); }
        if (rTrigger.Down()) { SetActiveColor(3); }
    }

    public void SetActiveColor(int color)
    {
        if (color == activeIndex || !levelColors) { return; }
        
        Color temp = levelColors.GetColor(color);
        if (temp != LevelColors.NONEXIST)
        {
            activeColor = temp;

            Image previous = hud.GetChild(activeIndex).GetComponent<Image>();
            previous.color = DISABLED;
            
            Image current = hud.GetChild(color).GetComponent<Image>();
            current.color = HIGHLIGHT;

            activeIndex = color;
        }
    }

    void OnLevelWasLoaded(int level)
    {
        Debug.Log("Called OnLevelWasLoaded");
        levelColors = GameObject.FindObjectOfType<LevelColors>();
        activeIndex = 0;
        SetActiveColor(0);
    }
}
