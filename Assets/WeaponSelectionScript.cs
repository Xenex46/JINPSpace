using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WeaponSelectionScript : MonoBehaviour
{
    [SerializeField]
    public PlayerController m_PlayerController = null;
    [SerializeField]
    public List<GameObject> m_SelectionSquares;
    [SerializeField]
    public List<Text> m_AmmoAmounts;


    // Update is called once per frame
    void Update()
    {
        for(int i =0; i < 5; i++)
        {
            m_SelectionSquares[i].SetActive(i == m_PlayerController.selectedWeapon);
        }

        for (int i = 1; i < 5; i++)
        {
            m_AmmoAmounts[i].text = m_PlayerController.m_Weapons.m_Weapons[i].m_Ammo.ToString();
        }

    }
}
