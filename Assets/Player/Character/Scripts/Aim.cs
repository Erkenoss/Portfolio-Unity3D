using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Aim : MonoBehaviour
{
    [SerializeField]
    private PlayerInput input;
    [SerializeField]
    private int priority = 10;
    private InputAction aimAction;
    private CinemachineVirtualCamera aimCamera;

    private void Awake() {
        aimCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = input.actions["Aim"];
    }

    private void StartAim() {
        aimCamera.Priority += priority;
    }

    private void CancelAim() {
        aimCamera.Priority -= priority;

    }


    #region Enable/Disable
    private void OnEnable() {
        aimAction.performed += x => StartAim();
        aimAction.canceled += x => CancelAim();
    }

    private void OnDisable() {
        aimAction.performed -= x => StartAim();
        aimAction.canceled -= x => CancelAim();
    }

    #endregion
}
