using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DestroyAndCountPigs : MonoBehaviour
{

    int countOfSattPigsHere;

    GameObject Score;

    private void Start()
    {
        Score = GameObject.Find("Score");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Animal"))
        {
            GameObject Animal = collision.gameObject;

            FoodHandler foodHandler = Animal.transform.GetComponent<FoodHandler>();

            if (foodHandler.satt)
            {
                Destroy(collision.gameObject);

                countOfSattPigsHere = countOfSattPigsHere + 1;
                Score.GetComponent<Score>().countOfSattPigsTotal = Score.GetComponent<Score>().countOfSattPigsTotal + 1;
            }
        }
    }
}
