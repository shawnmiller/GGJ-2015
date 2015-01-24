using UnityEngine;

public class GameManager : MSingleton<GameManager>
{
    private const string LEVEL_KEY = "Starting Level";
    public Canvas menu;
    public Canvas hud;
    public Canvas postLevel;
    public Canvas pause;

    private int currentLevel;

    void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(menu);
        DontDestroyOnLoad(hud);
        DontDestroyOnLoad(postLevel);
        DontDestroyOnLoad(pause);

        menu.enabled = true;
        hud.enabled = false;
        postLevel.enabled = false;
        pause.enabled = false;
    }

    public void StartGame()
    {
        currentLevel = GetStartLevel();
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
        pause.enabled = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        pause.enabled = false;
    }

    public void LevelComplete()
    {
        currentLevel++;
        PlayerPrefs.SetInt(LEVEL_KEY, currentLevel);

        hud.enabled = false;
        postLevel.enabled = true;
    }

    public void LevelFailed()
    {
        LoadLevel();
    }

    public void AdvanceLevel()
    {
        LoadLevel();
    }

    public void Back()
    {
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