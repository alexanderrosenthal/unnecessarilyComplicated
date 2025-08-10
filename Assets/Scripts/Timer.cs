using UnityEngine;
using System.Collections.Generic;

public class SimpleTimer : MonoBehaviour
{
    public List<GameObject> potentialScripts = new List<GameObject>();
    public float targetTime = 30.0f;
    private float countdown;           // interner Zähler

    //------------------- BASICS ---------------------------------------------------------
    void Start()
    {
        countdown = targetTime; // Startwert setzen
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
        int random = Random.Range(0, potentialScripts.Count);

        for (int i = 0; i < potentialScripts.Count; i++)
        {
            potentialScripts[i].SetActive(true);
        }
        targetTime = targetTime + targetTime;
    }
}