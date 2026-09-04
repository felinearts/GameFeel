using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private Vector3 _inputDirection;
    public float walkSpeed = 4;
    private Vector3 finalMovement;

    Quaternion playerRotation;

    public static Vector3 cameraOffset;
    public float cameraOffsetSpeed = 2;
    
    [Header("Camera")]
    public AnimationCurve camOffset_AccelerationCurve;
    public float camOffset_AccelerationSpeed = 1;
    private float camOffset_Acceleration = 0;
    bool isMoving = false;
    
    [Header("Animation")]
    public Animator animator;

    [Header("References")] [SerializeField]
    private Player_AttackController _attackController;
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + cameraOffset, 0.2f);
    }

    private void Update()
    {
        if (isMoving)
        {
            camOffset_Acceleration = Mathf.Clamp01(camOffset_Acceleration + camOffset_AccelerationSpeed * Time.smoothDeltaTime);
            
        }
        else
        {
            camOffset_Acceleration = 0;
            
        }
        
        cameraOffset += _inputDirection * (Time.smoothDeltaTime * (cameraOffsetSpeed * camOffset_AccelerationCurve.Evaluate(camOffset_Acceleration)));
        
        
        if(cameraOffset.magnitude > 1)
            cameraOffset.Normalize();
    }

    private void FixedUpdate()
    {

        if (_attackController.isAttacking)
            return;
        
        finalMovement = _inputDirection * (walkSpeed * Time.fixedDeltaTime);
        playerRotation = Quaternion.LookRotation(finalMovement, Vector3.up);
        _rigidbody.Move(transform.position + finalMovement, playerRotation);
    }

    public void InputMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _inputDirection.Set(input.x, 0,input.y);
            
            animator.SetFloat("walkSpeed", input.magnitude);

            isMoving = true;
        }

        if (context.canceled)
        {
            camOffset_Acceleration = 0;
            animator.SetFloat("walkSpeed", 0);
            _inputDirection = Vector3.zero;
            isMoving = false;
        }
    }

    public void InputAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _attackController.Attack(0);
        }
    }

}
