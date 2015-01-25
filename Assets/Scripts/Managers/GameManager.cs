using UnityEngine;
using UnityEngine.EventSystems;
using XInputDotNetPure;

public enum GameMode
{
    WeenieMode = 0,
    ManMode = 1
}

public enum ManModeState
{
    PissingRainbows,
    ScreamingViolently
}

public class GameManager : MSingleton<GameManager>
{
    private const string LEVEL_KEY = "Starting Level";
    public GameObject menu;
	public GameObject hud;
	public GameObject postLevel;
	public GameObject pause;

    private int currentLevel;

    private GameMode mode;
    public GameMode Mode
    { get { return mode; } }

    private ManModeState manState = ManModeState.ScreamingViolently;
    public ManModeState ManState
    { get { return manState; } }

    private GameObject background;
    public GameObject Background
    { get { return background; } }

	private bool levelCompleted;
	public bool LevelCompleted
	{
		get { return levelCompleted; }
	}

	private GamePadState state;
	public GamePadState State
	{
		get { return state; }
		set { state = value; }
	}
	public bool IsConnected
	{
		get { return state.IsConnected; }
	}

	private PaintGun paintGun;
	public PaintGun Painter
	{
		get { return paintGun; }
	}

	public ColorType PaintGun
	{
		get { return paintGun.cType; }
		set { paintGun.cType = value; }
	}

    void Awake()
    {
		PlayerPrefs.DeleteAll(); // Temp delete thissssss
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(menu);
        DontDestroyOnLoad(hud);
        DontDestroyOnLoad(postLevel);
        DontDestroyOnLoad(pause);

        menu.SetActive(true);
		hud.SetActive(false);
		postLevel.SetActive(false);
		pause.SetActive(false);

		paintGun = GetComponent<PaintGun>();
    }

	void Update()
	{
		// Run a global state in the game manager
		state = GamePad.GetState(PlayerIndex.One);

		if (state.Buttons.Start == ButtonState.Pressed || Input.GetKeyDown(KeyCode.Escape) && !menu.activeInHierarchy)
		{
			Pause();
		}

		if (mode == GameMode.ManMode)
		{
			if (state.Buttons.B == ButtonState.Pressed && manState != ManModeState.ScreamingViolently)
			{
				WreckShit();
			}
		}
	}

    public void StartGame(int mode)
    {
        this.mode = (GameMode)mode;

        currentLevel = GetStartLevel();
		menu.SetActive(false);
		hud.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        Debug.Break();
#else
        Application.Quit();
#endif
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pause.SetActive(true);
		EventSystem eventSys = GameObject.FindObjectOfType<EventSystem>();
		eventSys.SetSelectedGameObject(pause.transform.GetChild(0).GetChild(0).gameObject);
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        pause.SetActive(false);
    }

    public void LevelComplete()
    {
		levelCompleted = true;
        currentLevel++;
        PlayerPrefs.SetInt(LEVEL_KEY, currentLevel);

		if (GameManager.Instance.Mode == GameMode.ManMode)
		{
			paintGun.slider.value = 1f;
		}

        hud.SetActive(false);
        postLevel.SetActive(true);
        EventSystem eventSys = GameObject.FindObjectOfType<EventSystem>();
        eventSys.SetSelectedGameObject(postLevel.transform.GetChild(0).GetChild(0).gameObject);
    }

    public void LevelFailed()
    {
		if (GameManager.Instance.Mode == GameMode.ManMode)
		{
			paintGun.slider.value = 1f;
		}
        LoadLevel();
    }

    public void AdvanceLevel()
    {
		postLevel.SetActive(false);
		hud.SetActive(true);
        LoadLevel();
    }

    public void Back()
    {
		hud.SetActive(false);
		postLevel.SetActive(false);
		menu.SetActive(true);
		EventSystem eventSys = GameObject.FindObjectOfType<EventSystem>();
		eventSys.SetSelectedGameObject(menu.transform.GetChild(0).GetChild(0).gameObject);
        Application.LoadLevel(0);
    }

    public void WreckShit()
    {
        manState = ManModeState.ScreamingViolently;
		GameObject player = GameObject.Find("Player");
		player.GetComponent<PlayerRedo>().canDie = true;
    }

    public void PissBreak()
    {
        manState = ManModeState.PissingRainbows;
		GameObject player = GameObject.Find("Player");
		Debug.Log(player);
		player.GetComponent<PlayerRedo>().canDie = false;
    }

    private int GetStartLevel()
    {
        int level = PlayerPrefs.GetInt(LEVEL_KEY, -1);
        if (level == -1)
        {
            PlayerPrefs.SetInt(LEVEL_KEY, 1);
            PlayerPrefs.Save();
            return 1;
        }
        return level;
    }

    private void LoadLevel()
    {
		levelCompleted = false;
        Application.LoadLevel(currentLevel);
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0) { return; }
        background = GameObject.Find("Weenie Mode Background");
        if (mode == GameMode.ManMode)
        {
			PissBreak();
			background.SetActive(false);
			background.collider.enabled = false;
			paintGun.Initialize();
        }
    }

	public void GoToLevel()
	{
		GameObject text = GameObject.Find("InputField").transform.GetChild(2).gameObject;

		string levelText = text.GetComponent<UnityEngine.UI.Text>().text;

		int level = int.Parse(levelText);

		currentLevel = level;
		menu.SetActive(false);
		hud.SetActive(true);
		LoadLevel();
	}
}