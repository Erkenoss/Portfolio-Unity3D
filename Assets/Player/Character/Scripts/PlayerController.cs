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
    private bool tutorialCompleted;

    [SerializeField]
    private GameObject movementTutorial;
    [SerializeField]
    private GameObject summaryMovement;
    [SerializeField]
    public List<Transform> ZQSDList = new List<Transform>();

    [SerializeField]
    private GameObject cursorTutorial;
    [SerializeField]
    private GameObject summaryCursor;
    private bool cursorDone;
    private bool endCursor;


    #region Awake / Start
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

        //Tutorial Initialization
        tutorialCompleted = false;
        cursorDone = false;
        endCursor = false;

        TutorialListMovement();
        ActivateColor();

        //set the InputAction
        InitializeInput();
    }

    #endregion

    #region Update
    void Update()
    {
        if (!tutorialCompleted) {
            StartTutorial();
        }

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

    #region Tutorial/UITutorial
    private void StartTutorial() {

        StartTutorialMovement();

        bool activeMovement = AllImageActive();

        if (activeMovement) {
            summaryMovement.gameObject.SetActive(false);
            movementTutorial.gameObject.SetActive(false);
            ZQSDList.Clear();

            cursorDone = StartTutorialCursor();

            if (cursorDone) {
                endCursor = EndTutorialCursor();

                if (endCursor) {
                    tutorialCompleted = true;
                }
                else {
                    tutorialCompleted = false;
                }
            }

        }
    }


    private void StartTutorialMovement() {

        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        float horizontalMove = moveValue.y;
        float verticalMove = moveValue.x;

        //Enabled or disabled the BGColor during the tutorial for Tutorial Movement
        if (horizontalMove > 0) {
            if (ZQSDList.Count > 0) {
                ZQSDList[0].gameObject.SetActive(false);
            }
        }
        else if (horizontalMove < 0) {
            if (ZQSDList.Count > 0) {
                ZQSDList[2].gameObject.SetActive(false);
            }
        }
        else if (verticalMove > 0) {
            if (ZQSDList.Count > 0) {
                ZQSDList[3].gameObject.SetActive(false);
            }
        }
        else if (verticalMove < 0) {
            if (ZQSDList.Count > 0) {
                ZQSDList[1].gameObject.SetActive(false);
            }
        }
    }

    private bool AllImageActive () {
        foreach(Transform item in ZQSDList) {
            if (item.gameObject.activeSelf) {
                return false;
            }
        }
        return true;
    }
    private bool StartTutorialCursor() {

        cursorTutorial.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            cursorTutorial.gameObject.SetActive(false);

            return true;
        }

        return false;
    }

    private bool EndTutorialCursor() {

        summaryCursor.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.LeftAlt)) {

            IEnumerator coroutine = Disable(10.0f);
            StartCoroutine(coroutine);

            return true;
        }

        return false;
    }

    private IEnumerator Disable(float waitTime) {
        while(true) {

            yield return new WaitForSeconds(waitTime);

            summaryCursor.gameObject.SetActive(false);
        }
    }

    private void ActivateColor() {
        foreach (Transform image in ZQSDList) {
            image.gameObject.SetActive(true);
        }
    }

    private void TutorialListMovement() {
        if (movementTutorial != null) {

            ZQSDList.Clear();

            foreach (Transform image in movementTutorial.transform) {
                if (image.transform.IsChildOf(movementTutorial.transform)) {

                    Transform imageComponent = image.GetComponent<Transform>();

                    if (imageComponent != null) {
                        ZQSDList.Add(imageComponent);
                    }
                }
            }
        }
    }

    #endregion

}
