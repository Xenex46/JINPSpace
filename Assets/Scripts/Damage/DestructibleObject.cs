using UnityEngine;
using UnityEngine.Events;

public class DestructibleObject : MonoBehaviour
{
    public UnityEvent OnDestroyed = new UnityEvent();

    [SerializeField]
    private int m_TotalHealthpoints = 100;

    private int m_CurrentHealthpoints = 0;

    private bool m_IsDestroyed = false;

    [Header("Floating text")]
    [SerializeField]
    private FloatingText m_FloatingTextPrefab = null;

    private PrefabPool<FloatingText> m_FloatingTextPool = null;

    [Header("Healthbar")]
    [SerializeField]
    private Vector3 m_HealthbarOffset;

    [SerializeField]
    private Healthbar m_HealthbarPrefab = null;

    private Healthbar m_Healthbar = null;

    private void Awake()
    {
        Reset();
        m_FloatingTextPool = PoolManager.Instance.GetPool(m_FloatingTextPrefab);
    }

    public void ApplyDamage(DamageInfo damageInfo)
    {
        if (m_IsDestroyed)
        {
            return;
        }

        InstantiateFloatingText(
            damageInfo.DamageAmount.ToString(),
            damageInfo.Dealer.transform.position);

        m_CurrentHealthpoints -= damageInfo.DamageAmount;

        if (m_Healthbar == null)
        {
            PrefabPool<Healthbar> healthbarPool = PoolManager.Instance.GetPool(m_HealthbarPrefab);

            m_Healthbar = healthbarPool.Get();
            m_Healthbar.Reset(transform, m_HealthbarOffset);
        }

        m_Healthbar.SetPercentage((float)m_CurrentHealthpoints / m_TotalHealthpoints);

        if (m_CurrentHealthpoints <= 0)
        {
            m_IsDestroyed = true;

            m_Healthbar.ReturnToPool();
            m_Healthbar = null;

            OnDestroyed.Invoke();
        }
    }

    private void InstantiateFloatingText(string text, Vector3 position)
    {
        FloatingText damageText = m_FloatingTextPool.Get();

        position += Random.insideUnitSphere;

        damageText.Reset(text, position);
    }

    public void Reset()
    {
        m_CurrentHealthpoints = m_TotalHealthpoints;
        m_IsDestroyed = false;
    }

    public void Destroy()
    {
        if (m_IsDestroyed)
        {
            return;
        }

        m_IsDestroyed = true;

        m_Healthbar.ReturnToPool();
        m_Healthbar = null;

        OnDestroyed.Invoke();
    }
}
