using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 3.0f;
    [SerializeField]
    private float sprintSpeed = 8.0f;
    [SerializeField]
    private float blendTree;
    [SerializeField]
    private float targetRotation = 0.0f;
    public float rotationSmoothTime = 0.12f;
    private float targetVelocity;
    private float playerSpeed;

    private int Speed;
    private Animator animator;
    private float acceleration = 10.0f;
    private CharacterController controller;
    private PlayerInput input;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    //InputActions
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction lookAction;

    //cursor state
    private bool isMouseEnable = false;

    //tutorial section
    private bool tutorialMovement;
    [SerializeField]
    private GameObject canvasTutorialMovement;
    public List<Image> zqsdList = new List<Image>();



    #region Awake
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        animator  = GetComponent<Animator>();

        //Reference to the float on the animator
        Speed = Animator.StringToHash("Speed");

        //Camera.main = search for the camera with the tag mainCamera
        cameraTransform = Camera.main.transform;
        Cursor.visible = false;

        tutorialMovement = false;
        TutorialListMovement();

        //set the InputAction
        InitializeInput();
    }

    private void Start() {
        Tutorial();
    }

    #endregion

    #region Update
    void Update()
    {
        HandleCursor();
        Move();
    }

    #endregion

    #region InputAction
    private void InitializeInput() {
        moveAction = input.actions["Move"];
        sprintAction = input.actions["Sprint"];
        lookAction = input.actions["Look"];
    }

    #endregion

    #region Move
    //Base:
    //Move function in the Unity Documentation
    //Move function in the ThirdPersonStarterAsset
    //Some changes for adapt with the new input system
    //There is no jump. So the control of the groundCheck is here
    private void Move() {
        //Control if the player is on the ground by the CharacterController
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            //If yes, my vertical velocity is equal to 0
            playerVelocity.y = 0f;
        }

        //Take the value of my input
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        float targetSpeed;

        //Initialized the player speed. Idle, Walk or Run
        if (moveInput == Vector2.zero) {
            targetSpeed = 0.0f;
        }
        else {
            targetSpeed = sprintAction.ReadValue<float>() > 0 ? sprintSpeed : walkSpeed;
        }

        float move = new Vector3(moveInput.x, 0, moveInput.y).magnitude;
        float speedOffset = 0.1f;
        float inputMagnitude = 1f;

        //transform the speed smoothly by using Lerp. And finally Round for precision
        if (move < targetSpeed - speedOffset || move > targetSpeed + speedOffset) {
            playerSpeed = Mathf.Lerp(move, targetSpeed * inputMagnitude, Time.deltaTime * acceleration);
            playerSpeed = Mathf.Round(playerSpeed * 1000) / 1000f;
        }
        else {
            //Else, the speed of my player is equal to 0
            playerSpeed = targetSpeed;
        }

        //Variable to tell my animator. Same as player speed, using Lerp for interpolate
        blendTree = Mathf.Lerp(blendTree, targetSpeed, Time.deltaTime * acceleration);
        if (blendTree < 0.01f){
            blendTree = 0f;
        }

        //Take the direction of my input
        Vector3 inputDirection = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized;

        if (moveInput != Vector2.zero) {
            //To orient smoothly my player in this direction using Atan2() for calculate the angle by tangent between x and z axis and SmoothDampAngle() for smooth rotation to this angle
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref targetVelocity, rotationSmoothTime);

            //Finally use Quaternion.Euler for orient my player to the angle of rotation
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        //rotate toward the camera direction
        //Quaternion.Euler: NEED (x, y, z) in the case of rotation of the player base on the camera position, we use only the y axis
        //Calculate smoothly the direction and add force with Vector3.forward. So, in the direction choose by the player
        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        //Finally, move the CharacterController
        controller.Move(targetDirection.normalized * (playerSpeed * Time.deltaTime) + new Vector3(0.0f, playerVelocity.y, 0.0f) * Time.deltaTime);

        //Update the animation with the bleedTree variable
        if (animator) {
            animator.SetFloat("Speed", blendTree);
        }
    }

    #endregion

    #region Cursor

    //Check if cursor ir active or not
    private void HandleCursor() {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            isMouseEnable = !isMouseEnable;
        }
        if (isMouseEnable) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            DisableLookInput();
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            EnableLookInput();
        }
    }

    //LookInput is active when there is no cursor
    private void EnableLookInput() {
        lookAction.Enable();
    }

    //LookInput is unactive when there is cursor
    private void DisableLookInput() {
        lookAction.Disable();
    }

    #endregion


    private void Tutorial() {
        while(!tutorialMovement) {
            Time.timeScale = 0f;
            zqsdList[0].color = Color.red; //A Changer
        }

    }

    private void TutorialListMovement() {
        if (canvasTutorialMovement != null) {

            zqsdList.Clear();

            foreach (Transform movement in canvasTutorialMovement.transform) {

                Image imageComponent = movement.GetComponent<Image>();

                if (imageComponent != null) {
                    zqsdList.Add(imageComponent);
                }
            }
            canvasTutorialMovement.gameObject.SetActive(true);
        }
    }

}
