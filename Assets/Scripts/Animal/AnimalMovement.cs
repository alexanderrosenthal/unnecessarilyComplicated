using UnityEngine;
using System.Collections.Generic;

public class AnimalMovement : MonoBehaviour
{
    [Header("GameDesign")]
    public float moveSpeed = 5f;  // Geschwindigkeit in Einheiten pro Sekunde

    [Header("By Code")]
    public Transform target;
    public Transform home;
    public bool onTheWayTowards = true;

    //------------------- BASICS ---------------------------------------------------------
    void Start()
    {
        Transform spotsT = GameObject.Find("Spots").transform;

        int random = Random.Range(0, spotsT.GetComponent<SpotHandler>().takingInputTargets.Count);
        //Zielt auf den AnimalContacter und mag den gesamt Spot nicht (Y-Position?)
        target = spotsT.GetComponent<SpotHandler>().takingInputTargets[random].GetChild(0);
    }

    void Update()
    {
        if (onTheWayTowards)
        {
            Move(target);
        }
        else if (!onTheWayTowards)
        {
            Move(home);
        }
    }

    //------------------- INDIVIDUAL ---------------------------------------------------------
    private bool Move(Transform moveTarget)
    {
        if (moveTarget == null) return false;

        // Schrittweite pro Frame
        float step = moveSpeed * Time.deltaTime;

        // Bewege das Objekt gleichmäßig in Richtung Ziel
        transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, step);

        //Schwein auf Ziel ausrichten
        transform.LookAt(moveTarget);

        return true;
    }
}