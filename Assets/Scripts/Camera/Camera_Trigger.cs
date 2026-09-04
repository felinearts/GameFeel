using System;
using Unity.Cinemachine;
using UnityEngine;

public class Camera_Trigger : MonoBehaviour
{
    [SerializeField] private CinemachineCamera camera;
    
    public void EnableCamera()
    {
        camera.Prioritize();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            EnableCamera();
    }
}
