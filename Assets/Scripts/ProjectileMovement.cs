using UnityEngine;

public class ProjectileMovement : APooledObject
{
    [SerializeField]
    public bool m_IsPickup = false;

    [SerializeField]
    private float m_MovementSpeed = 10f;

    [SerializeField]
    private float m_Acceleration = 0;

    [SerializeField]
    private Rigidbody m_Rigidbody = null;

    [SerializeField]
    private TrailRenderer m_TrailRenderer = null;

    [SerializeField]
    private bool m_DestroyOnImpact = true;

    [SerializeField]
    private LineRenderer m_LineRenderer = null;

    [SerializeField]
    private float m_DestroyAfter = 0;

    [SerializeField]
    private Explosion m_Explosion = null;

    private PrefabPool<Explosion> m_Pool = null;

    private float m_AliveTime = 0;

    private float m_AdditionalMovementSpeed = 0;

    private Vector3 m_StartingPosition;
    private void Awake()
    {
        if (m_Explosion != null)
        {
            m_Pool = PoolManager.Instance.GetPool(m_Explosion);
        }
    }

    private void FixedUpdate()
    {
        m_AdditionalMovementSpeed += m_Acceleration * Time.fixedDeltaTime;
        Vector3 targetPosition = m_Rigidbody.position + transform.forward * (m_MovementSpeed + m_AdditionalMovementSpeed) * Time.fixedDeltaTime;
        m_Rigidbody.MovePosition(targetPosition);

        if (m_LineRenderer != null)
        {
            m_LineRenderer.SetPosition(0, (2 * m_Rigidbody.position + m_StartingPosition) / 3);
            m_LineRenderer.SetPosition(1, m_Rigidbody.position);
        }
    }
    private void Update()
    {
        m_AliveTime += Time.deltaTime;

        if (m_DestroyAfter > 0 && m_DestroyAfter <= m_AliveTime)
        {
            ReturnToPool();
        }
    }

    public void HandleObjectHit(bool applyDamage, DamageInfo damgeInfo)
    {
        if (m_IsPickup && damgeInfo.Dealer != null)
        {
            Debug.Log("Pickup hit projectile");
            return;
        }
        if (m_Explosion != null)
        {
            Explosion explosion = m_Pool.Get();
            explosion.Restart(m_Rigidbody.position);
        }
        if (m_DestroyOnImpact)
        {
            ReturnToPool();
        }
    }

    public void Restart(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        m_Rigidbody.position = position;
        m_Rigidbody.rotation = rotation;

        m_AliveTime = 0;
        m_AdditionalMovementSpeed = 0;

        gameObject.SetActive(true);
        if (m_TrailRenderer != null)
        { 
            m_TrailRenderer.Clear();
        }
        if (m_LineRenderer != null)
        {
            m_StartingPosition = m_Rigidbody.position;
            m_LineRenderer.SetPosition(0, m_Rigidbody.position);
            m_LineRenderer.SetPosition(1, m_Rigidbody.position);
        }
    }
}
