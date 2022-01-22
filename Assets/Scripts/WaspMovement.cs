using System.Collections;
using UnityEngine;

public class WaspMovement : APooledObject
{
    private readonly int m_AttackAnimatorHash = Animator.StringToHash("Attack");

    private readonly int m_DeathAnimatorHash = Animator.StringToHash("Death");

    [Header("Misc")]
    [SerializeField]
    private DestructibleObject m_DestructibleObject = null;

    [SerializeField]
    private int m_ScoreValue = 999;

    [SerializeField]
    private Animator m_Animator = null;

    [Header("Movement")]
    [SerializeField]
    private Vector2 m_MovementSpeed;

    [SerializeField]
    private float m_SideMovementAmplitude = 1f;

    [SerializeField]
    private Rigidbody m_Rigidbody = null;

    [Header("Attack")]
    [SerializeField]
    private WeaponList m_Weapons = null;

    [SerializeField]
    private float m_AttackTimeSeconds = 2f;

    private Coroutine m_AttackCoroutine = null;

    private void SubscribeToAnimationEvents()
    {
        EnemyAnimationEvent[] events = m_Animator.GetBehaviours<EnemyAnimationEvent>();

        for (int i = 0; i < events.Length; i++)
        {
            events[i].OnAnimationPointReached += HandleAnimationEvent;
        }
    }

    private void HandleAnimationEvent(EEnemyAnimationEvent animationEvent)
    {
        switch (animationEvent)
        {    
            case EEnemyAnimationEvent.Attack:
                HandleAttackAnimationEvent();
                break;
            case EEnemyAnimationEvent.Death:
                HandleDeathAnimationEvent();
                break;
            default:
                break;
        }
    }

    private void HandleAttackAnimationEvent()
    {
        m_Weapons.FireAll();
    }

    private void HandleDeathAnimationEvent()
    {
        GameplayManager.Instance.HandlePointsAdded(m_ScoreValue);

        if (m_AttackCoroutine != null)
        {
            StopCoroutine(m_AttackCoroutine);
            m_AttackCoroutine = null;
        }

        ReturnToPool();
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.forward * m_MovementSpeed.y + 
            new Vector3(Mathf.Sin(Time.time * m_MovementSpeed.x) * m_SideMovementAmplitude, 0f, 0f);

        Vector3 targetPosition = m_Rigidbody.position + direction * Time.fixedDeltaTime;
        m_Rigidbody.MovePosition(targetPosition);
    }

    public void HandleDestroyed()
    {
        m_Animator.SetTrigger(m_DeathAnimatorHash);
    }

    public void Reset(Vector3 worldPosition)
    {
        transform.position = worldPosition;
        gameObject.SetActive(true);

        SubscribeToAnimationEvents();
        m_DestructibleObject.Reset();
        m_AttackCoroutine = StartCoroutine(AttackCoroutine());

    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_AttackTimeSeconds);
            m_Animator.SetTrigger(m_AttackAnimatorHash);
        }
    }
}
