using UnityEngine;
using UnityEngine.Events;

public struct DamageInfo
{
    public int DamageAmount;
    public int AmmoAmount;
    public DamageDealer Dealer;
}

public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private int m_DamageMin = 10;

    [SerializeField]
    private int m_DamageMax = 20;

    public UnityEvent<bool, DamageInfo> OnObjectHit = new UnityEvent<bool, DamageInfo>();

    private void OnTriggerEnter(Collider other)
    {
        DamageHandler handler = other.GetComponent<DamageHandler>();

        DamageInfo damageInfo = new DamageInfo()
        {
            DamageAmount = GetDamage(),
            AmmoAmount = Random.Range(1, 5),
            Dealer = this
        };

        bool applyDamage = handler != null && damageInfo.DamageAmount != 0;

        if (applyDamage)
        {
            handler.ApplyDamage(damageInfo);
        }

        OnObjectHit.Invoke(applyDamage, damageInfo);
    }

    private int GetDamage()
    {
        if (m_DamageMax < m_DamageMin)
        {
            Debug.LogError($"Max {m_DamageMax} is larger thatn Min {m_DamageMin}!");
            return 0;
        }

        int damage = Random.Range(m_DamageMin, m_DamageMax);
        return damage;
    }
}
