using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator = null;

    [SerializeField]
    private float m_Speed = 100f;

    [SerializeField]
    private WeaponList m_Weapons = null;

    [SerializeField]
    private Rigidbody m_Rigidbody = null;

    private float m_Vertical = 0f;

    private float m_Horizontal = 0f;

    private bool m_Shoot = false;

    private readonly int m_HorizontalAnimatorHash = Animator.StringToHash("Horizontal");

    private readonly int m_VerticalAnimatorHash = Animator.StringToHash("Vertical");

    private void GetInput()
    {
        m_Horizontal = Input.GetAxis("Horizontal");
        m_Vertical = Input.GetAxis("Vertical");

        m_Shoot = Input.GetButton("Shoot");
    }

    private void UpdateAnimator()
    {
        m_Animator.SetFloat(m_HorizontalAnimatorHash, m_Horizontal);
        m_Animator.SetFloat(m_VerticalAnimatorHash, m_Vertical);
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = m_Rigidbody.position +  new Vector3(m_Horizontal, 0f, m_Vertical) * Time.deltaTime * m_Speed;
        if (newPosition[0] > 35) newPosition[0] = 35;
        if (newPosition[0] < -35) newPosition[0] = -35;
        if (newPosition[2] > 25) newPosition[2] = 25;
        if (newPosition[2] < -30) newPosition[2] = -30;
        m_Rigidbody.MovePosition(newPosition);
    }

    private void Update()
    {
        GetInput();
        UpdateAnimator();

        if (m_Shoot)
        {
            m_Weapons.FireAll();
        }
    }

    public void HandleDestroyed()
    {
        GameplayManager.Instance.HandlePlayerDestroyed();
        Destroy(gameObject);
    }
}
