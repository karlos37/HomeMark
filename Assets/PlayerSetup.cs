using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
	private float lightingLevel;
	private float volumeLevel;

	private Vector3 TargetPosition;
	private Quaternion TargetRotation;

	public GameObject cameraObj;
	private PhotonView view;
	private CharacterController charControl;
	private float speed;

	// Start is called before the first frame update
	void Start()
	{
		lightingLevel = 50f;
		volumeLevel = 50f;
		view = GetComponent<PhotonView>();
		charControl = GetComponent<CharacterController>();
		speed = 5f;
	}

	// Update is called once per frame
	void Update()
	{
		if (view.IsMine)
		{
			RegularMove();
			cameraObj.SetActive(true);
		}
		else
		{
			SmoothMove();
			cameraObj.SetActive(false);
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			TargetPosition = (Vector3)stream.ReceiveNext();
			TargetRotation = (Quaternion)stream.ReceiveNext();
		}
	}

	private void RegularMove()
	{
		//Get horizontal and Vertical movements
		float horComp = horComp = Input.GetAxis("Vertical");
		float vertComp = vertComp = Input.GetAxis("Horizontal") * -1;

		Vector3 moveVect = Vector3.zero;

		//Get look Direction
		Vector3 cameraLook = cameraObj.transform.forward;
		cameraLook.y = 0f;
		cameraLook = cameraLook.normalized;

		Vector3 forwardVect = cameraLook;
		Vector3 rightVect = Vector3.Cross(forwardVect, Vector3.up).normalized * -1;

		moveVect += rightVect * horComp;
		moveVect += forwardVect * vertComp;

		moveVect *= speed;


		charControl.SimpleMove(moveVect);
	}

	private void SmoothMove()
	{
		transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.01f);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, 500 * Time.deltaTime);
	}

	void SetLightingLevel(float lightingLevel)
	{
		this.lightingLevel = lightingLevel;
	}

	void SetVolumeLevel(float volumeLevel)
	{
		this.volumeLevel = volumeLevel;
	}
}
