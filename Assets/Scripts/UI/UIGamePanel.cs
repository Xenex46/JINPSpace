using UnityEngine;
using UnityEngine.UI;

public class UIGamePanel : MonoBehaviour
{
    [SerializeField]
    private Text m_PointsText = null;

    [SerializeField]
    private GameObject m_PauseMenuRoot = null;

    public void Start()
    {
        PlayerPrefs.SetInt("score", 0);
    }

    public void SetPoints(int points)
    {
        m_PointsText.text = points.ToString();
    }

    public void SetPauseMenuVisible(bool visible)
    {
        m_PauseMenuRoot.SetActive(visible);
    }
}
