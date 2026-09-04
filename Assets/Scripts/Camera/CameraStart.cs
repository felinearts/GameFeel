using System;
using UnityEngine;

public class CameraStart : MonoBehaviour
{
    public Camera_Trigger initialCamera;

    private void Awake()
    {
        initialCamera.EnableCamera();
    }
}
