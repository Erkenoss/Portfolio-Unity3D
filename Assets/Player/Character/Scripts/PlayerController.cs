using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 3.0f;
    [SerializeField]
    private float sprintSpeed = 8.0f;
    [SerializeField]
    private float jumpHeight = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 8.0f;
    [SerializeField]


    private CharacterController controller;
    private PlayerInput input;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();

        //Camera.main = search for the camera with the tag mainCamera
        cameraTransform = Camera.main.transform;

        InitializeInput();
    }

    void Update()
    {
        Move();

    }
    private void InitializeInput() {
        moveAction = input.actions["Move"];
        jumpAction = input.actions["Jump"];
        sprintAction = input.actions["Sprint"];
    }

    private void Move() {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float speed = sprintAction.ReadValue<float>() > 0 ? sprintSpeed : playerSpeed;

        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;


        controller.Move(move * Time.deltaTime * speed);

        //change the height position of thee player
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            //Play fall animation
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (speed == sprintSpeed) {
            //Play Run Animation
        }
        else {
            //Play Walk Animation
        }
        //rotate toward the camera direction
        //Quaternion.Euler: NEED (x, y, z) in the case of rotation of the player base on the camera position, we use only the y axis
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        //Quaternion.Lerp: NEED (directionFrom.rotation, directionTo.rotation, speed)
        //This fonction interpolate for smooth effect and turn the character smoothly in the directionTo calculate by Euler just before
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
