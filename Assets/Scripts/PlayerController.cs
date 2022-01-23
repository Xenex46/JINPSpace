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

    private bool m_Weapon1 = false;

    private bool m_Weapon2 = false;

    private bool m_Weapon3 = false;

    private bool m_Weapon4 = false;

    private bool m_Weapon5 = false;

    private int selectedWeapon = 0;

    private readonly int m_HorizontalAnimatorHash = Animator.StringToHash("Horizontal");

    private readonly int m_VerticalAnimatorHash = Animator.StringToHash("Vertical");

    private void GetInput()
    {
        m_Horizontal = Input.GetAxis("Horizontal");
        m_Vertical = Input.GetAxis("Vertical");

        m_Shoot = Input.GetButton("Shoot");

        m_Weapon1 = Input.GetButton("Weapon1");
        m_Weapon2 = Input.GetButton("Weapon2");
        m_Weapon3 = Input.GetButton("Weapon3");
        m_Weapon4 = Input.GetButton("Weapon4");
        m_Weapon5 = Input.GetButton("Weapon5");
    }

    private void UpdateAnimator()
    {
        m_Animator.SetFloat(m_HorizontalAnimatorHash, m_Horizontal);
        m_Animator.SetFloat(m_VerticalAnimatorHash, m_Vertical);
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = m_Rigidbody.position +  new Vector3(m_Horizontal, 0f, m_Vertical) * Time.deltaTime * m_Speed;
        //if (newPosition[0] > 35) newPosition[0] = 35;
        //if (newPosition[0] < -35) newPosition[0] = -35;
        if (newPosition[2] > 30) newPosition[2] = 30;
        if (newPosition[2] < -32) newPosition[2] = -32;
        //-58 30 : -35 -32
        if(62 * newPosition[0] + 23 * newPosition[2] > -2906 && -62 * newPosition[0] + 23 * newPosition[2] > -2906 && newPosition[2] >= -32 && newPosition[2] <= 30)
            m_Rigidbody.MovePosition(newPosition);
    }

    private void Update()
    {
        GetInput();
        UpdateAnimator();

        if (m_Weapon1)
        {
            selectedWeapon = 0;
        }
        else if (m_Weapon2)
        {
            selectedWeapon = 1;
        }
        else if (m_Weapon3)
        {
            selectedWeapon = 2;
        }
        else if (m_Weapon4)
        {
            selectedWeapon = 3;
        }
        else if (m_Weapon5)
        {
            selectedWeapon = 4;
        }

        if (m_Shoot)
        {
            m_Weapons.Fire(selectedWeapon);
        }
    }

    public void HandleDestroyed()
    {
        GameplayManager.Instance.HandlePlayerDestroyed();
        Destroy(gameObject);
    }
}
