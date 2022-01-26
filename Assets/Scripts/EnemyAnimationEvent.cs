using System;
using UnityEngine;

public enum EEnemyAnimationEvent
{ 
    Attack  = 0,
    Death   = 1
}

public class EnemyAnimationEvent : StateMachineBehaviour
{
    [SerializeField]
    private EEnemyAnimationEvent m_Event;

    [SerializeField]
    private float m_NormalizedTimeToFire = 0.5f;

    public event Action<EEnemyAnimationEvent> OnAnimationPointReached;

    private bool m_EventFired = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_EventFired = false;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_EventFired = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= m_NormalizedTimeToFire && !m_EventFired)
        {
            m_EventFired = true;
            OnAnimationPointReached.Invoke(m_Event);
        }
    } 
}
