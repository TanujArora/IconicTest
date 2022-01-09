using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController
{
	private const float FOLLOW_DIST_BACK = -10f;
	private const float FOLLOW_DIST_UP = 10f;
	private Transform cameraTrans;
	private CharacterController character;

	public CameraController(Camera camera, CharacterController character)
	{
		this.cameraTrans = camera.transform;
		this.character = character;
	}

	public void LateUpdate()
	{
        cameraTrans.position = character.transform.position + new Vector3(0, FOLLOW_DIST_UP, FOLLOW_DIST_BACK);
		cameraTrans.LookAt(character.transform);
	}
}
