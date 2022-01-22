using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private ProjectileMovement m_ProjectilePrefab = null;

    [SerializeField]
    private List<Transform> m_Emitters = null;

    [SerializeField]
    private float m_RateOfFire = 10f;

    private PrefabPool<ProjectileMovement> m_Pool = null;

    private Coroutine m_LimiterCoroutine = null;

    private void Awake()
    {
        m_Pool = PoolManager.Instance.GetPool(m_ProjectilePrefab);
    }

    public bool Fire()
    {
        if (m_LimiterCoroutine != null)
        {
            return false;
        }

        m_LimiterCoroutine = StartCoroutine(RateOfFireLimiterCoroutine());
        return true;       
    }

    private IEnumerator RateOfFireLimiterCoroutine()
    {
        foreach (Transform emitter in m_Emitters)
        {
            ProjectileMovement projectileInstance = m_Pool.Get();
            projectileInstance.Restart(emitter.position, emitter.rotation);
        }

        yield return new WaitForSeconds(1f / m_RateOfFire);

        m_LimiterCoroutine = null;
    }
}
