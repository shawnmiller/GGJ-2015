using UnityEditor;
using UnityEngine;
using System.IO;

public static class MenuFunctions
{
    public static string lastLevel = "";

    [MenuItem("GGJ/Play")]
    public static void StartFromMenu()
    {
        lastLevel = EditorApplication.currentScene;
        EditorApplication.OpenScene("Assets/Scenes/MainMenu.unity");
        EditorApplication.isPlaying = true;
    }

    [MenuItem("GGJ/Return")]
    public static void ReturnToLast()
    {
        if (lastLevel != "")
        {
            EditorApplication.isPlaying = false;
            EditorApplication.OpenScene(lastLevel);
        }
    }

    [MenuItem("GGJ/Edit Current")]
    public static void EditCurrent()
    {
        string current = Application.loadedLevelName;
        string path = Directory.GetFiles(Application.dataPath, current + ".unity", SearchOption.AllDirectories)[0];
        Debug.Log(path);
        path = path.Substring(path.IndexOf("Assets"));
        Debug.Log(path);
        EditorApplication.isPlaying = false;
        EditorApplication.OpenScene(path);
    }
}