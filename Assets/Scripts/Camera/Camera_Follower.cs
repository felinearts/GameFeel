using System;
using UnityEngine;

public class Camera_Follower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform targetFollower;

    public float playerOffsetScale = 1;

    public Bounds bounds;

    private Vector3 finalPosition;
    private Vector3 target_WorldToLocalPosition;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(bounds.center, bounds.size);
        bounds.center = transform.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //--get player
        target = PlayerInstance.GetPlayer().transform;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target || !targetFollower)
            return;

        target_WorldToLocalPosition = transform.InverseTransformDirection(target.position) + (Player.cameraOffset * playerOffsetScale);
        
        finalPosition.Set(
            Mathf.Clamp(target_WorldToLocalPosition.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(target_WorldToLocalPosition.y, bounds.min.y, bounds.max.y),
            Mathf.Clamp(target_WorldToLocalPosition.z, bounds.min.z, bounds.max.z)
        );
        
        targetFollower.position = finalPosition;
    }
}
