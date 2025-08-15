using UnityEngine;
using System.Collections.Generic;

public class SimpleTimer : MonoBehaviour
{
    [Header("GameDesign")]
    public float targetTime = 30.0f;

    [Header("LevelSetup")]
    public List<GameObject> potentialScripts = new();

    //PRIVATE
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
        for (int i = 0; i < potentialScripts.Count; i++)
        {
            potentialScripts[i].SetActive(true);
        }
        targetTime += targetTime;
    }
}