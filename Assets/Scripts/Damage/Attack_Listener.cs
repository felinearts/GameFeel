using System;
using UnityEngine;

public class Attack_Listener : MonoBehaviour
{
    [SerializeField] private Mob _mob;

    private Attack_HitBox _hitBox;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Attack_HitBox>(out _hitBox))
        {
            _mob.OnDamage(_hitBox.GetAttackInstance());
        }
    }
}
