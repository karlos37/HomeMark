using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Rigidbody rigidBody;
    private Camera cam;
    private float xInput;
    private float zInput;
    [SerializeField] float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Get input for movement
         xInput = Input.GetAxis("Horizontal");
         zInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        //rigidBody.velocity = new Vector3(xInput * speed, rigidBody.velocity.y, zInput*speed);
    }

    private void OnPointerEnter()
    {
        print("Entered Object");
    }

    private void OnPointerExit()
    {
        print("Pointered exited object");
    }
}
