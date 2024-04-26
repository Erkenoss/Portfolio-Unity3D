using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    PlayerInput input;

    [Header("Movement")]
    public Vector2 move;
    public bool isSprinting;
    public float moveSpeed = 3.0f;
    public float sprintSpeed = 8.0f;

    [Header("Cam and Cursor")]
    [SerializeField] private Transform followTarget;
    public Vector2 look;
    public bool isMouseEnable = false;
    private float xRotation;
    private float yRotation;


    private void Awake()
    {
        //Create new instance of generated input actions scripts
        input = new PlayerInput();

        //Movement walk and sprint
        input.Player.Move.performed += x => move = x.ReadValue<Vector2>();
        input.Player.Sprint.performed += x => isSprinting = x.ReadValue<bool>();

        //camera controller
        input.Player.Look.performed += x => look = x.ReadValue<Vector2>();
    }

    void Update()
    {
        HandleCursor();
    }

    private void LateUpdate() {
        CameraRotation();
    }

    private void CameraRotation() {
        if (isMouseEnable) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            xRotation += look.y * Time.deltaTime;
            yRotation -= look.x * Time.deltaTime;

            xRotation = Mathf.Clamp(xRotation, -30, 70);

            Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);

            followTarget.rotation = rotation;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void HandleCursor() {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            isMouseEnable = !isMouseEnable;
        }
    }


    #region -> Enable/Disable

    private void OnEnable() {
        input.Enable();
    }

    private void OnDisable() {
        input.Disable();
    }

    #endregion
}
