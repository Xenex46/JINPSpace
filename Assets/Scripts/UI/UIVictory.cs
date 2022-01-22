using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIVictory : MonoBehaviour
{
    [SerializeField]
    private Text m_PointsText = null;
    [SerializeField]
    private Text m_HighScoreText = null;
    [SerializeField]
    private Text m_NewHighScoreText = null;
    public void Start()
    {
        m_PointsText.text = PlayerPrefs.GetInt("score").ToString();
        if(PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("highScore"))
        {
            m_NewHighScoreText.text = "New High Score!";
            PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("score"));
        }
        else
        {
            m_NewHighScoreText.text = "";
        }
        m_HighScoreText.text = "High Score: "  + PlayerPrefs.GetInt("highScore").ToString();
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
