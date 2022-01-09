using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CharacterController characterController;
    private CameraController cameraController;

    private void Start()
    {
        cameraController = new CameraController(Camera.main, characterController);
    }

    private void LateUpdate()
    {
        cameraController.LateUpdate();
    }
}
