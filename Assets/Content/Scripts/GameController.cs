using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CharacterController characterController;

    private CollectableController collectableController;
    private UIController uiController;
    private GateInteractable gateInteractable;

    private CameraController cameraController;

    private int[] password;

    private void Start()
    {
        cameraController = new CameraController(Camera.main, characterController);
        
        StarController starController = GetComponent<StarController>();
        uiController = new UIController(transform.Find("UI"), characterController, starController);
        collectableController = new CollectableController(transform.Find("Collectables"), characterController, uiController);

        gateInteractable = GameObject.Find("Gate").GetComponent<GateInteractable>();
        gateInteractable.OnCollidedWithGate += GateInteractable_OnCollidedWithGate;
        UpdateClues();
    }

    private void GateInteractable_OnCollidedWithGate(IInteractableAction action)
    {
        uiController.DisplayGateUI(password, action);
    }

    private void UpdateClues()
    {
        password = new int[] { Random.Range(0, 9), Random.Range(0, 9), Random.Range(0, 9) };

        ClueCollectable[] clues = collectableController.getCollectableList<ClueCollectable>(CollectableType.CLUE);

        for (int i = 0; i < password.Length; i++)
        {
            clues[i].SetClueValue(i + 1, password[i]);
        }

    }

    private void LateUpdate()
    {
        cameraController.LateUpdate();
    }
}
