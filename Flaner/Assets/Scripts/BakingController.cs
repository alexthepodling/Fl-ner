using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

class Ingredient
{
    // The type of the ingredient
    // 0 - flour; 1 - salt; 2 - oil
    public int type;

    // The name of the ingredient
    public string name;

    // The quantity of the ingredient in the player's cupboard
    public float quantity;

    public Ingredient(int _type, string _name, float _quantity)
    {
        type = _type;
        name = _name;
        quantity = _quantity;
    }
}

public class BakingController : MonoBehaviour
{
    // A boolean which tells us if the player is baking
    public bool isBaking = false;

    // An integer which tells us what stage of the baking the player is in
    private int bakingState = 0;

    // An array of cameras for the baking process
    [SerializeField] private CinemachineVirtualCamera[] bakingCameras;

    // A list of all the ingredients in the cupboard
    private List<Ingredient> cupboardIngredients = new List<Ingredient>() {
        new Ingredient(0, "Self-raising flour", 1000),
        new Ingredient(1, "Table salt", 1000),
        new Ingredient(2, "Olive oil", 1000)
    };

    // A reference to the prefab for shelf ingredients
    [SerializeField] private GameObject ingredientObj;

    // A reference to the shelf in the scene
    [SerializeField] private Transform shelf;

    // A list of the ingredients that the player has taken out of the cupboard
    private List<Ingredient> counterIngredients;

    // A list of all the ingredients used by the player in the current bake
    private List<Ingredient> usedIngredients;

    // A list of all the gameobjects on the shelf at the moment
    private List<GameObject> shelfObjects = new List<GameObject>();

    // An integer that tracks what ingredient we are looking at right now
    private int currentIngredient;

    // A variable that tracks what we are weighing
    private Ingredient weighingIngredient;

    // A variable that tracks whether we are weighing an ingredient
    private bool isWeighing = false;

    // A float of how much we have weighed
    private float weighedAmount = 0f;
    
    // Start-up of the program
    void Start()
    {
        // Initialize the shelf for the first time
        InitShelf();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBaking)
        {
            // Change baking state
            if (Input.GetButtonDown("Horizontal"))
            {
                bakingState += Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));

                bakingState = Mathf.Clamp(bakingState, 0, 4);
            }

            // Turn on/off baking cameras according to the baking state
            for (int i = 0; i < bakingCameras.Length; i++)
            {
                if (i != bakingState)
                {
                    bakingCameras[i].enabled = false;
                }
                else
                {
                    bakingCameras[i].enabled = true;
                }
            }

            // Switch the baking state
            switch (bakingState)
            {
                // Ingredients
                case 0:
                    if (!isWeighing)
                    {
                        CycleIngredients(cupboardIngredients);

                        // Select ingredient to weigh
                        if (Input.GetButtonDown("Jump"))
                        {
                            weighingIngredient = cupboardIngredients[currentIngredient];
                            isWeighing = true;
                        }
                    }
                    else
                    {
                        // The player's input affects the amoung weighed out
                        weighedAmount += Input.GetAxisRaw("Vertical");

                        // The weighed amount cannot go below 0 or exceed the quantity in the cupboard
                        weighedAmount = Mathf.Clamp(weighedAmount, 0f, weighingIngredient.quantity);

                        if (Input.GetButtonDown("Fire1"))
                        {
                            isWeighing = false;
                        }

                        if (Input.GetButtonDown("Jump"))
                        {
                            counterIngredients.Add(new Ingredient(weighingIngredient.type, weighingIngredient.name, weighedAmount));
                            isWeighing = false;
                        }
                    }

                    break;

                // Mixing
                case 1:
                    CycleIngredients(counterIngredients);

                    if (Input.GetButtonDown("Jump"))
                    {

                    }

                    break;

                // Kneading + shaping
                case 2:

                    break;

                // Scoring
                case 3:

                    break;

                // Baking
                case 4:

                    break;

            }
        }

        UpdateShelf();
    }

    // Init shelf - call to initialize or reinitialize the shelf
    private void InitShelf()
    {
        // Get rid of any existing shelfobjects
        if (shelfObjects.Count > 0)
        {
            foreach (GameObject shelfObject in shelfObjects)
            {
                Destroy(shelfObject);
            }
        }

        // Clear the shelfobjects list
        shelfObjects.Clear();

        // Add a new shelfObj for each ingredient in the cupboard
        for (int i = 0; i < cupboardIngredients.Count; i++)
        {
            shelfObjects.Add(Instantiate(ingredientObj, shelf.position + new Vector3(0f, 0.5f, i * 0.6f - 2f), Quaternion.identity));
        }
    }

    // Update shelf - call to update the items on the shelf
    private void UpdateShelf()
    {
        // Loop through all the shelf objs and change their quantity to match the corresponding cupboard item
        for (int i = 0; i < cupboardIngredients.Count; i++)
        {
            shelfObjects[i].transform.Find("ItemName").gameObject.GetComponent<TMP_Text>().text = cupboardIngredients[i].name;
            shelfObjects[i].transform.Find("ItemQuantity").gameObject.GetComponent<TMP_Text>().text = cupboardIngredients[i].quantity.ToString();
        }
    }

    // Cycle through ingredients with key press
    void CycleIngredients(List<Ingredient> ingredientsList)
    {
        if (Input.GetButtonDown("Vertical"))
        {
            currentIngredient += Mathf.RoundToInt(Input.GetAxis("Vertical"));

            // Loop back around to start / end of array if exceeded
            if (currentIngredient < 0)
            {
                currentIngredient = ingredientsList.Count - 1;
            }
            else if (currentIngredient >= ingredientsList.Count)
            {
                currentIngredient = 0;
            }
        }
    }
}
