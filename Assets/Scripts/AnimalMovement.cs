using UnityEngine;
using System.Collections.Generic;

public class AnimalMovement : MonoBehaviour
{
    public Transform target;
    public Transform home;
    public Transform animal;
    public Transform animalBody;
    public float moveSpeed = 5f;  // Geschwindigkeit in Einheiten pro Sekunde

    public bool onTheWayTowards = true;

    public List<Transform> potentialTargets = new List<Transform>();

    void Start()
    {
        Transform spotsT = GameObject.Find("Spots").transform;

        for (int i = 0; i < spotsT.childCount; i++)
        {
            Transform child = spotsT.GetChild(i);

            if (child.GetChild(0).GetComponent<CubeHandler>().takingInput == true)
            {
                potentialTargets.Add(child.GetChild(1).transform);
            }
        }

        int random = Random.Range(0, potentialTargets.Count);
        target = potentialTargets[random];
    }

    void Update()
    {
        if (onTheWayTowards)
        {
            if (target == null) return;

            // Schrittweite pro Frame
            float step = moveSpeed * Time.deltaTime;
 
            // Bewege das Objekt gleichmäßig in Richtung Ziel
            animal.position =Vector3.MoveTowards(transform.position, target.position, step);

            animalBody.LookAt(target);
        }
        else if (onTheWayTowards == false)
        {
            if (home == null) return;

            // Schrittweite pro Frame
            float step = moveSpeed * Time.deltaTime;

            // Bewege das Objekt gleichmäßig in Richtung Ziel
            animal.position = Vector3.MoveTowards(transform.position, home.position, step);

            animalBody.LookAt(home);
        }
    }
}