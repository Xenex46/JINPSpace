using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIVictory : MonoBehaviour
{
    [SerializeField]
    private Text m_PointsText = null; 

    public void Start()
    {
        m_PointsText.text = PlayerPrefs.GetInt("score").ToString();
    }

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
        SceneLoader.LoadScene(SceneNamesConsts.SPACE_SCENE_NAME, HandleSceneLaoded);
    }

    private void HandleSceneLaoded(Scene scene)
    {
        Debug.Log($"Loaded scene {scene.name}!");
    }
}
