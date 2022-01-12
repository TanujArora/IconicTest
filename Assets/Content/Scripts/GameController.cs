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
        UpdateClues();
    }

    private void UpdateClues()
    {
        int[] randomNumbers = new int[] { Random.Range(0, 9), Random.Range(0, 9), Random.Range(0, 9) };

        ClueCollectable[] clues = collectableController.getCollectableList<ClueCollectable>(CollectableType.CLUE);

        for (int i = 0; i < randomNumbers.Length; i++)
        {
            clues[i].SetClueValue(i + 1, randomNumbers[i]);
        }

    }

    private void LateUpdate()
    {
        cameraController.LateUpdate();
    }
}
