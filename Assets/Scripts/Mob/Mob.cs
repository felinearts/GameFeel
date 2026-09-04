using System;
using Unity.Cinemachine;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] Animator animator;


    private Vector3 pushback_Direction;
    private float pushback_force;
    private AttackDamageData _damageData;
    private bool pushback;
    

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + pushback_Direction);
    }

    private void FixedUpdate()
    {
        if (pushback)
        {
            UpdatePushback();
        }
        else
        {
            
        }
        
    }

    void UpdatePushback()
    {
        pushback_force *= Mathf.Exp(-_damageData.decayRate * Time.fixedDeltaTime);
        
        _rigidbody.MovePosition(transform.position + pushback_Direction * ( pushback_force * Time.fixedDeltaTime));

        if (pushback_force <= 0)
            pushback = false;
    }

    public void OnDamage(AttackInstance attack)
    {
        //--guardar pushback
        _damageData = attack.damage;
        
        //--obtener direccion pushback
        pushback_Direction = transform.position - attack.origin;
        pushback_Direction.y = 0;
        pushback_Direction.Normalize();
        
        _damageData.GenerateFXAt(transform.position, pushback_Direction);
        
        pushback_force = _damageData.force;
        pushback = true;
        
        animator.Play("Damage",0,0);
    }
}
