using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // A list of all the ingredients in the cupboard
    private List<Ingredient> cupboardIngredients = new List<Ingredient>() {
        new Ingredient(0, "Self-raising flour", 1000),
        new Ingredient(1, "Table salt", 1000),
        new Ingredient(2, "Olive oil", 1000)
    };

    // A list of the ingredients that the player has taken out of the cupboard
    private List<Ingredient> counterIngredients;

    // A list of all the ingredients used by the player in the current bake
    private List<Ingredient> usedIngredients;

    // An integer that tracks what ingredient we are looking at right now
    private int currentIngredient;

    // A variable that tracks what we are weighing
    private Ingredient weighingIngredient;

    // A variable that tracks whether we are weighing an ingredient
    private bool isWeighing = false;

    // A float of how much we have weighed
    private float weighedAmount = 0f;

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

            // Switch the baking state
            switch(bakingState)
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
