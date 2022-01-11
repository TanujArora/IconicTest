using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CharacterController characterController;

    private CollectableController collectableController;


    private CameraController cameraController;

    private void Start()
    {
        cameraController = new CameraController(Camera.main, characterController);
        collectableController = new CollectableController(transform.Find("Collectables"), characterController);
    }

    private void LateUpdate()
    {
        cameraController.LateUpdate();
    }
}
