using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    [Header("GameDesign")]

    public float Intensityvalue = 1f;
    public float movementStartValue = 20f;
    public float movementStartValueNegativ = -20f;
    public float lerpSpeed = 2f;

    [Header("LevelSetup")]
    public Transform parentOfMovingObjects;

    //PRIVATE
    private List<GameObject> MovingObjects = new();
    private Vector3[] targetPositions;

    public float movementValue = 20f;
    public float movementValueNegativ = -20f;


    //------------------- BASICS ---------------------------------------------------------
    void Start()
    {
        parentOfMovingObjects = GameObject.Find("Bushes").transform;

        //Stellt Liste der Spawnpoints zusammen
        for (int i = 0; i < parentOfMovingObjects.childCount; i++)
        {
            MovingObjects.Add(parentOfMovingObjects.GetChild(i).gameObject);
        }

        //Setzt Startwerte, die dann über die Intensity geändert werden.
        movementValue = movementStartValue;
        movementValueNegativ = movementStartValueNegativ;

        //Löst ab Start regelmässig zufällige Bewegunen aus
        StartCoroutine(TriggerRoutine());

        //Steigert mit der Zeit die Intensität der Bewegungen
        StartCoroutine(IntensityRoutine());
    }

    //------------------- INDIVIDUALS ---------------------------------------------------------
    IEnumerator TriggerRoutine()
    {
        while (true)
        {
            // Warte zufällige Zeit
            float waitTime = 2f;
            yield return new WaitForSeconds(waitTime);

            // Aktion ausführen
            TriggerAction();
        }
    }

    void TriggerAction()
    {
        // Startziele setzen
        targetPositions = new Vector3[MovingObjects.Count];

        for (int i = 0; i < MovingObjects.Count; i++)
        {
            float RandomX = Random.Range(movementValueNegativ, movementValue);
            float RandomZ = Random.Range(movementValueNegativ, movementValue);

            targetPositions[i] = MovingObjects[i].transform.position + new Vector3(RandomX, 0, RandomZ);
        }

        // Bewege jeden Busch sanft zu seiner Zielposition
        for (int i = 0; i < MovingObjects.Count; i++)
        {
            MovingObjects[i].transform.position = Vector3.Lerp(
                MovingObjects[i].transform.position,
                targetPositions[i],
                Time.deltaTime * lerpSpeed
            );
        }
    }

    IEnumerator IntensityRoutine()
    {
        while (true)
        {
            // Warte zufällige Zeit
            float waitTime = 10f;
            yield return new WaitForSeconds(waitTime);

            // Aktion ausführen
            IntensityAction();
        }
    }
    void IntensityAction()
    {
        movementValue += Intensityvalue;
        movementValueNegativ -= Intensityvalue;
    }
}