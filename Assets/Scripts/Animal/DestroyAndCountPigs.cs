using UnityEngine;

public class DestroyAndCountPigs : MonoBehaviour
{
    GameObject AnimalSpawn;
    GameObject Score;

    private void Start()
    {
        Score = GameObject.Find("Score");
        AnimalSpawn = GameObject.Find("Animals");
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


                Score.GetComponent<Score>().countOfSattPigsTotal = Score.GetComponent<Score>().countOfSattPigsTotal + 1;

                AnimalSpawn.GetComponent<AnimalSpawn>().maxPigsCount = AnimalSpawn.GetComponent<AnimalSpawn>().maxPigsCount - 1;
            }
        }
    }
}