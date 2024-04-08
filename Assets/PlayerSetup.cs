using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
	private Vector3 TargetPosition;
	private Quaternion TargetRotation;

	public GameObject cameraObj;
	public GameObject reticle;
	public VideoPlayer[] videoPlayers;

	private PhotonView view;
	private CharacterController charControl;
	private MenuCharacterMenuController menuControl;

	private float speed;

	public float defaultBrightness = 1f;

	public Light localLight;

	// Start is called before the first frame update
	void Start()
	{
		view = GetComponent<PhotonView>();
		charControl = GetComponent<CharacterController>();
		menuControl = GetComponent<MenuCharacterMenuController>();
		speed = 5f;

		videoPlayers = FindObjectsOfType<VideoPlayer>();

		localLight = FindObjectOfType<Light>();
		if (localLight == null)
		{
			Debug.LogError("No Light component found in the scene!");
		}
		else
		{
			localLight.intensity = defaultBrightness;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (view.IsMine)
		{
			RegularMove();
			cameraObj.SetActive(true);
			menuControl.enabled = true;
		}
		else
		{
			SmoothMove();
			cameraObj.SetActive(false);
			menuControl.enabled = false;
		}
	}

	public void ChangeBrightness(float amt)
	{
		// Debug.Log("<color=blue>Brightness: </color>" + localLight.intensity);
		localLight.intensity += amt;
	}

	public void ChangeVolume(float amt)
	{
		foreach (var videoPlayer in videoPlayers)
		{
			float new_vol;
			float cur_vol = videoPlayer.GetDirectAudioVolume(0);
			new_vol = Mathf.Clamp(cur_vol + amt, 0, 1);
			videoPlayer.SetDirectAudioVolume(0, new_vol);
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
}
