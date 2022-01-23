using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : APooledObject
{
    [SerializeField]
    private float m_Time = 1f;

    private float m_TimeAlive = 0f;

    // Update is called once per frame
    void Update()
    {
        m_TimeAlive += Time.deltaTime;

        if (m_Time <= m_TimeAlive)
            ReturnToPool();
    }

    public void Restart(Vector3 position)
    {
        m_TimeAlive = 0;
        transform.position = position;

        gameObject.SetActive(true);
    }
}
