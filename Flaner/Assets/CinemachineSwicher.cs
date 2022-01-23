using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CinemachineSwicher : MonoBehaviour
{
    [SerializeField] private InputAction action;

    private bool playerCam = true;

    private Animator animator;

    [SerializeField] private CinemachineVirtualCamera vcam1; //player
    [SerializeField] private CinemachineVirtualCamera vcam2; //baking


    private void Awake()
    {
       //    animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        action.performed += _ => SwitchPriority();
    }

    private void SwitchState()
    {
        if(playerCam)
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
        if(playerCam)
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
