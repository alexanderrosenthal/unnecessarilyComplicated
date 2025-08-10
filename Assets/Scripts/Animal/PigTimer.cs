using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigTimer : MonoBehaviour
{
    public float targetTime = 240.0f;
    private float countdown;           // interner Zähler
    public List<GameObject> partOfPigs = new List<GameObject>();

    GameObject AnimalSpawn;

    void Start()
    {
        countdown = targetTime; // Startwert setzen

        AnimalSpawn = GameObject.Find("Animals");
    }

    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0.0f)
        {
            timerEnded();
            countdown = targetTime; // Timer zurücksetzen
        }
    }


    void timerEnded()
    {
        if (!gameObject.GetComponent<FoodHandler>().satt)
        {
            //Deaktivieren Movement
            gameObject.transform.GetChild(0).gameObject.SetActive(false);

            // Dreht um 180 Grad um die X-Achse
            transform.Rotate(180f, 0f, 0f, Space.Self);

            //Change Farbe

            for (int i = 0; i < partOfPigs.Count; i++)
            {
                partOfPigs[i].GetComponent<Renderer>().material.color = Color.green;
            }

            //Change pig counter
            AnimalSpawn.GetComponent<AnimalSpawn>().maxPigsCount = AnimalSpawn.GetComponent<AnimalSpawn>().maxPigsCount - 1;
        }
    }
}