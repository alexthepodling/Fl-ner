using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    [SerializeField] float turnSpeed = 20f;

    public Transform cam;

    public float fl_Gravity = 15f;
    private Vector3 V3_move_direction = Vector3.zero;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // The camera that is used in regular play
    [SerializeField] private CinemachineFreeLook playerCam;

    // Whether or not the player can move
    public bool canMove = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (canMove)
            {
                controller.Move(moveDir.normalized * speed * Time.deltaTime);

            }
        }
        else
        {
            V3_move_direction.y -= fl_Gravity * Time.deltaTime;
        }

        if (canMove)
        {
            controller.Move(V3_move_direction);
            playerCam.enabled = true;
        }
        else
        {
            playerCam.enabled = false;
        }
    }
}
