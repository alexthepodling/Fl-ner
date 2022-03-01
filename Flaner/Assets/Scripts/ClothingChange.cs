using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingChange : MonoBehaviour
{
    private Renderer charcRenderer;
    public GameObject character;
    [SerializeField] private Color newColour;

    // Start is called before the first frame update
    void Start()
    {
        charcRenderer = character.GetComponent<Renderer>();
    }

    public void ChangeMaterial()
    {
        charcRenderer.material.color = newColour;

    }
}
