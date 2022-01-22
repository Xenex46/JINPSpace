using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuController : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        if (!PlayerPrefs.HasKey("highScore"))
            PlayerPrefs.SetInt("highScore", 0);
        SceneLoader.LoadScene(SceneNamesConsts.SPACE_SCENE_NAME, HandleSceneLaoded);
    }

    private void HandleSceneLaoded(Scene scene)
    {
        Debug.Log($"Loaded scene {scene.name}!");
    }
}
