using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_FixedSpeed = new Vector2();

    [SerializeField]
    private float m_PlayerSpeedModifier = 0;

    [SerializeField]
    private Renderer m_Renderer = null;

    private Material m_Material = null;

    private void Start()
    {
        m_Material = m_Renderer.material;
    }

    void Update()
    {
        Vector2 playerMovement = new Vector2(-Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical")) * m_PlayerSpeedModifier;
        m_Material.mainTextureOffset += (playerMovement + m_FixedSpeed) * Time.deltaTime;
    }
}
