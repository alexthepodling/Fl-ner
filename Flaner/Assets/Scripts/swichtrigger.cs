using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swichtrigger : MonoBehaviour
{
    // A reference to the baking controller
    [SerializeField] private BakingController bakingController;

    // Whether or not the zone is interactable
    private bool interactable = false;

    // A reference to the player character
    private GameObject playerChar;

    void Start()
    {
        // Find the player char by the tag "player"
        playerChar = GameObject.FindWithTag("Player");
    }

    // If the player enters the interactable area, then turn on interactable
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerChar)
        {
            interactable = true;
        }
    }

    // If the player leaves the interactable area, then turn off interactable
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerChar)
        {
            interactable = false;
        }
    }

    private void Update()
    {
        // If the player is in the interactable area and they press the interact key, then toggle whether the player is baking
        if (interactable && Input.GetButtonDown("Interact"))
        {
            bakingController.isBaking = !bakingController.isBaking;
            playerChar.GetComponent<PlayerMovement>().canMove = !playerChar.GetComponent<PlayerMovement>().canMove;
        }
    }
}
