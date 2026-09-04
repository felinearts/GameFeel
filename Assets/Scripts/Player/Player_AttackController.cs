using System;
using Unity.Cinemachine;
using UnityEngine;

public class Player_AttackController : MonoBehaviour
{
        [SerializeField] private Animator animator;
        [SerializeField] private Attack_HitBox hitbox;
        
        public string attackEndClipName;
        
        public bool isAttacking {get; private set;}
        private int attackId = 0;

        private AttackData currentAttack;

        public AttackData[] attacks;

        public AttackDamageData weaponData;

        private double attackStartTime;
        
        private void Update()
        {
                if (!isAttacking)
                        return;
                
                if (Time.timeSinceLevelLoadAsDouble > attackStartTime + currentAttack.attackDuration)
                {
                        isAttacking = false;
                }
        }

        public void Attack(int attackId)
        {
                if (attackId >= attacks.Length)
                        return;
                
                
                isAttacking = true;
                
                animator.Play(attacks[attackId].animationClipName);

                attackStartTime = Time.timeSinceLevelLoadAsDouble;
                currentAttack = attacks[attackId];

                AttackInstance attackInstance = new AttackInstance();
                attackInstance.attack = currentAttack;
                attackInstance.origin = transform.position;
                attackInstance.damage = weaponData;
                
                hitbox.SetAttack(attackInstance);
        }
}

public struct AttackInstance
{
        public AttackData attack;
        public AttackDamageData damage;
        public Vector3 origin;
}

[System.Serializable]
public struct AttackData
{
        public string animationClipName;
        public float attackDuration;
}

[System.Serializable]
public struct AttackDamageData
{
        public float force;
        public float decayRate;
        public float impulseMagnitude;

        public CinemachineImpulseSource cm_Impulse;
        public ParticleSystem hitParticles;
        
        public void GenerateFXAt(Vector3 position, Vector3 direction)
        {
                if (cm_Impulse != null)
                        cm_Impulse.GenerateImpulseAt(position, direction.normalized * impulseMagnitude);

                if (hitParticles != null)
                {
                        hitParticles.transform.position = position;
                        hitParticles.Emit(1);
                }
                        
                
                
                
        }
}

