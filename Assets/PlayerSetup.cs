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
	public XRCardboardReticle reticle;
	public VideoPlayer[] videoPlayers;

	private PhotonView view;
	private CharacterController charControl;
	private MenuCharacterMenuController menuControl;
	public GameObject light;

	private GameObject burger;
	private GameObject drink;

	private GameObject myBurger;
	private GameObject myDrink;

	private float speed;

	public float defaultBrightness = 1f;

	public Light localLight;

	private RaycastHit hit;

	private GameObject currentTarget = null;

	// Start is called before the first frame update
	void Start()
	{
		view = GetComponent<PhotonView>();
		charControl = GetComponent<CharacterController>();
		menuControl = GetComponent<MenuCharacterMenuController>();
		speed = 5f;

		myBurger = null;
		myDrink = null;
		burger = GameObject.Find("Burger");
		drink = GameObject.Find("Drink");

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
		transform.GetChild(0).GetChild(0).position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		if (view.IsMine)
		{
			RegularMove();
			cameraObj.SetActive(true);
			light.SetActive(true);
			menuControl.enabled = true;

			Vector3 rayStart = cameraObj.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0));
			if (Physics.Raycast(rayStart, cameraObj.transform.forward, out hit, 50))
			{
				GameObject hitObject = hit.collider.gameObject;

				if (hitObject != currentTarget)
                {
					if(hitObject.layer == 3)
                    {
						if (currentTarget != null && currentTarget.GetComponent<Outline>() != null && currentTarget.GetComponent<Outline>().enabled)
						{
							currentTarget.GetComponent<Outline>().enabled = false;
						}
						currentTarget = hitObject;
						if (currentTarget.GetComponent<Outline>() != null)
						{
							currentTarget.GetComponent<Outline>().enabled = true;
						}
						reticle.OnStartHover(2f);
                    }

					else
                    {
						if (currentTarget != null)
						{
							reticle.OnEndHover();
							if (currentTarget.GetComponent<Outline>() != null)
							{
								currentTarget.GetComponent<Outline>().enabled = false;
							}
							currentTarget = null;
						}
					}
                }

				//if (Input.GetButtonDown("js2"))
				if (Input.GetKeyDown("x"))
				{
					if (currentTarget.name == "PlayCollider")
					{
						view.RPC("PlayVideo", RpcTarget.All);
					}
					else if (currentTarget.name == "PauseCollider")
					{
						view.RPC("PauseVideo", RpcTarget.All);
					}
					else if (currentTarget.name.Contains("Armchair"))
					{
						view.RPC("Sit", RpcTarget.All);
					}
					else if (currentTarget.name == "Snack Machine")
					{
						view.RPC("SpawnBurger", RpcTarget.All);
					}
					else if (currentTarget.name == "Drink Machine")
					{
						view.RPC("SpawnDrink", RpcTarget.All);
					}
					else if (currentTarget.name.Contains("Drink") && myDrink == null)
					{
						myDrink = currentTarget;
					}
					else if (currentTarget.name.Contains("Burger") && myBurger == null)
					{
						myBurger = currentTarget;
					}
				}
			}
			else if (currentTarget != null)
			{
				reticle.OnEndHover();
				if (currentTarget.GetComponent<Outline>() != null)
				{
					currentTarget.GetComponent<Outline>().enabled = false;
				}
				currentTarget = null;
            }
			if (myDrink != null)
			{
				view.RPC("MoveDrink", RpcTarget.All);
			}
			if (myBurger != null)
			{
				view.RPC("MoveBurger", RpcTarget.All);
			}
		}
		else
		{
			SmoothMove();
			cameraObj.SetActive(false);
			light.SetActive(false);
			menuControl.enabled = false;
		}
	}

	[PunRPC]
	public void PlayVideo()
	{
		GameObject.Find("Theater").transform.Find("Remote Control").Find("VideControl Canvas").Find("Play Button").gameObject.GetComponent<Button>().onClick.Invoke();
	}

	[PunRPC]
	public void PauseVideo()
	{
		GameObject.Find("Theater").transform.Find("Remote Control").Find("VideControl Canvas").Find("Pause Button").gameObject.GetComponent<Button>().onClick.Invoke();
	}

	[PunRPC]
	public void SpawnDrink()
	{
		GameObject newdrink = Instantiate(drink, new Vector3(-26f, 5f, 0.5f), new Quaternion(0, 0, 0, 0));
		newdrink.GetComponent<Outline>().enabled = false;
		newdrink.SetActive(true);
	}

	[PunRPC]
	public void SpawnBurger()
	{
		GameObject newburger = Instantiate(burger, new Vector3(-26f, 5f, 23f), new Quaternion(0, 0, 0, 0));
		newburger.GetComponent<Outline>().enabled = false;
		newburger.SetActive(true);
	}

	[PunRPC]
	public void MoveBurger()
	{
		myBurger.transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z + 2f);
	}

	[PunRPC]
	public void MoveDrink()
	{
		myDrink.transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z + 2f);
	}

	[PunRPC]
	public void Sit()
	{
		charControl.enabled = false;
		transform.position = new Vector3(currentTarget.transform.position.x, currentTarget.transform.position.y + 0.5f, currentTarget.transform.position.z);
		transform.rotation = Quaternion.LookRotation(currentTarget.transform.forward);
		charControl.enabled = true;
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
