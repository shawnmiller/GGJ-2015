using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XInputDotNetPure;

public class ColorSwapper : MSingleton<ColorSwapper>
{
    private static Color DISABLED = new Color(1f, 1f, 1f, 0f);
    private static Color HIGHLIGHT = new Color(1f, 1f, 1f, 0.2f);
    private LevelColors levelColors;
    //private ColorType activeColor;
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

        if (lBumper.Pressed()) { SetActiveColor(0); }
        else if (rBumper.Pressed()) { SetActiveColor(1); }
        else if (lTrigger.Pressed()) { SetActiveColor(2); }
        else if (rTrigger.Pressed()) { SetActiveColor(3); }
        else { SetActiveColor(-1); }
    }

    public void SetActiveColor(int color)
    {
        if (color == activeIndex || !levelColors) { return; }

        ColorType cType = levelColors.GetColor(color);

        if (GameManager.Instance.Mode == GameMode.WeenieMode)
        {
            GameManager.Instance.Background.GetComponent<ActiveColorType>().Type = cType;
        }

        if (cType != LevelColors.NONEXIST)
        {
            //activeColor = cType;

			if (activeIndex != -1)
			{
				Image previous = hud.GetChild(activeIndex).GetComponent<Image>();
				previous.color = DISABLED;
			}

			Image current = hud.GetChild(color).GetComponent<Image>();
			current.color = HIGHLIGHT;
        }

		activeIndex = color;
    }

    void OnLevelWasLoaded(int level)
    {
        levelColors = GameObject.FindObjectOfType<LevelColors>();
        activeIndex = -1;
        SetActiveColor(-1);
    }
}
