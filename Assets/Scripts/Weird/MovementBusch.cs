using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBusch : MonoBehaviour
{
    public List<GameObject> Busches = new List<GameObject>();
    public Vector3[] targetPositions;

    Transform buschParent;
    public float moveSpeed = 2f;  // Geschwindigkeit in Einheiten pro Sekunde

    float movementValue = 1f;
    float movementValueNegativ = -1f;
    public float lerpSpeed = 2f;

    void Start()
    {
        buschParent = GameObject.Find("Büsche").transform;

        //Stellt Liste der Spawnpoints zusammen
        for (int i = 0; i < buschParent.childCount; i++)
        {
            Busches.Add(buschParent.GetChild(i).gameObject);
        }

        StartCoroutine(TriggerRoutine());
        StartCoroutine(IntensityRoutine());
    }

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
        for (int i = 0; i < Busches.Count; i++)
        {
            float RandomX = Random.Range(movementValueNegativ, movementValue);
            float RandomY = Random.Range(movementValueNegativ, movementValue);

            targetPositions[i] = Busches[i].transform.position + new Vector3(RandomX, 0, RandomY);
        }

        // Bewege jeden Busch sanft zu seiner Zielposition
        for (int i = 0; i < Busches.Count; i++)
        {
            Busches[i].transform.position = Vector3.Lerp(
                Busches[i].transform.position,
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
        movementValue = movementValue + 1f;
        movementValueNegativ = movementValueNegativ - 1f;
    }
}