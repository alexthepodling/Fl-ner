using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swichtrigger : MonoBehaviour
{
    public GameObject camSwich;

    private void Start()
    {
        camSwich.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        camSwich.SetActive(true);

    }

    private void OnTriggerExit(Collider other)
    {
        camSwich.SetActive(false);

    }
}
