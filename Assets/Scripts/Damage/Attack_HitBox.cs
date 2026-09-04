using UnityEngine;

public class Attack_HitBox : MonoBehaviour
{
    private AttackInstance _attackInstance;

    public void SetAttack(AttackInstance attack)
    {
        _attackInstance = attack;
    }

    public AttackInstance GetAttackInstance()
    {
        return _attackInstance;
    }
}
