using UnityEngine;

public class GameManager : MSingleton<GameManager>
{
    private const string LEVEL_KEY = "Starting Level";
    public GameObject menu;
	public GameObject hud;
	public GameObject postLevel;
	public GameObject pause;

    private int currentLevel;

    void Awake()
    {
		PlayerPrefs.DeleteAll();
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(menu);
        DontDestroyOnLoad(hud);
        DontDestroyOnLoad(postLevel);
        DontDestroyOnLoad(pause);

        menu.SetActive(true);
		hud.SetActive(false);
		postLevel.SetActive(false);
		pause.SetActive(false);
    }

    public void StartGame()
    {
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
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        pause.SetActive(false);
    }

    public void LevelComplete()
    {
        currentLevel++;
        PlayerPrefs.SetInt(LEVEL_KEY, currentLevel);

        hud.SetActive(false);
        postLevel.SetActive(true);
    }

    public void LevelFailed()
    {
        LoadLevel();
    }

    public void AdvanceLevel()
    {
		postLevel.SetActive(false);
        LoadLevel();
    }

    public void Back()
    {
		hud.SetActive(false);
        Application.LoadLevel(0);
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
        Application.LoadLevel(currentLevel);
    }
}