using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : MonoBehaviour
{
    // Whether or not the zone is interactable
    public GameObject Switcher;

    public void Awake()
    {
        Switcher.SetActive(false);
    }

    // If the player enters the interactable area, then turn on interactable
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Switcher.SetActive(true);
        }
    }

    // If the player leaves the interactable area, then turn off interactable
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Switcher.SetActive(false);
        }
    }
}
