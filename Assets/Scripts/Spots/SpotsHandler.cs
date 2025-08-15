using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotsHandler : MonoBehaviour
{
    [Header("By Code")]
    public List<Transform> takingInputTargets = new();

    //------------------- BASICS ---------------------------------------------------------
    void Awake()
    {
        //Aaufbau der Liste mit allen Spots auf takingInput
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.GetComponent<SpotHandler>().takingInput == true)
            {
                takingInputTargets.Add(child.transform);
            }
        }
    }
}