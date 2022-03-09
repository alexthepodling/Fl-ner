using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class Sitting : MonoBehaviour
{
    [SerializeField] private InputAction action;

    private bool playerCam = true;

    private Animator animator;

    [SerializeField] private CinemachineFreeLook vcam1; //player
    [SerializeField] private CinemachineVirtualCamera vcam2; //sitting

    public GameObject interactBut;

    private void Awake()
    {
        //    animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        action.Enable();
        Cursor.lockState = CursorLockMode.None;
        interactBut.SetActive(true);
    }

    private void OnDisable()
    {
        action.Disable();
        Cursor.lockState = CursorLockMode.Locked;
        interactBut.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        action.performed += _ => SwitchPriority();
    }

    private void SwitchState()
    {
        if (playerCam)
        {
            animator.Play("BakingCamera");
        }
        else
        {
            animator.Play("PlayerCamera");
        }
        playerCam = !playerCam;
    }

    private void SwitchPriority()
    {
        if (playerCam)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }
        else
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
        }
        playerCam = !playerCam;
    }
}