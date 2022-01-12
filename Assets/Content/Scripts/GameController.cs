using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CharacterController characterController;
    public bool hardMode;

    private CollectableController collectableController;
    private UIController uiController;
    private GateInteractable gateInteractable;

    private CameraController cameraController;

    private int[] password;

    private void Awake()
    {
        if (hardMode)
        {
            int startHealth = characterController.MaxHealth / 2;
            characterController.SetHealth(startHealth);
        }
        else
        {
            characterController.SetHealth(characterController.MaxHealth);
        }
    }

    private void Start()
    {
        cameraController = new CameraController(Camera.main, characterController);
        
        StarController starController = GetComponent<StarController>();
        uiController = new UIController(transform.Find("UI"), characterController, starController);
        collectableController = new CollectableController(transform.Find("Collectables"), characterController, uiController);

        gateInteractable = GameObject.Find("Gate").GetComponent<GateInteractable>();
        gateInteractable.OnCollidedWithGate += GateInteractable_OnCollidedWithGate;
        UpdateClues();

        characterController.OnGameOver += CharacterController_OnGameOver;
    }

    private void CharacterController_OnGameOver()
    {
        uiController.DisplayGameOverUI();
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

    private void Update()
    {
        uiController.Update();
    }

    private void LateUpdate()
    {
        cameraController.LateUpdate();
    }
}
