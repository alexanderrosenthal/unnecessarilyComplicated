using System.Collections.Generic;
using UnityEngine;

public class PigTimer : MonoBehaviour
{
    [Header("GameDesign")]
    public float targetTime = 240.0f;

    [Header("PrefabSetup")]
    public List<GameObject> partOfPigs = new List<GameObject>();

    private float countdown;           // interner Zähler
    private GameObject Spawns;

    //------------------- BASICS ---------------------------------------------------------
    void Start()
    {
        countdown = targetTime; // Startwert setzen

        Spawns = GameObject.Find("Spawns");
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

    //------------------- INDIVIDUALS ---------------------------------------------------------
    void timerEnded()
    {
        if (!gameObject.GetComponent<FoodHandler>().satt)
        {
            //Deaktivieren Movement Component
            gameObject.GetComponent<AnimalMovement>().enabled = false;

            // wechselt Schwein in Sammlung toter Schweine
            transform.SetParent(transform.parent.parent.GetChild(1));

            // Dreht um 180 Grad um die X-Achse
            transform.Rotate(180f, 0f, 0f, Space.Self);

            //Verändert Farbe
            for (int i = 0; i < partOfPigs.Count; i++)
            {
                partOfPigs[i].GetComponent<Renderer>().material.color = Color.green;
            }

            //Change pig counter
            Spawns.GetComponent<AnimalSpawn>().maxPigsCount = Spawns.GetComponent<AnimalSpawn>().maxPigsCount - 1;
        }
    }
}