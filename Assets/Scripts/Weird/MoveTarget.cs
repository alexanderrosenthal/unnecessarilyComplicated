using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public List<GameObject> Busches = new List<GameObject>();
    public Vector3[] targetPositions;
    Transform buschParent;
    public float lerpSpeed = 2f;
    float movementValue = 20f;
    float movementValueNEgativ = -20f;

    void Start()
    {
        buschParent = GameObject.Find("Spots").transform;

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
        // Startziele setzen
        targetPositions = new Vector3[Busches.Count];

        for (int i = 0; i < Busches.Count; i++)
        {
            float RandomX = Random.Range(movementValueNEgativ, movementValue);
            float RandomZ = Random.Range(movementValueNEgativ, movementValue);

            targetPositions[i] = Busches[i].transform.position + new Vector3(RandomX, 0, RandomZ);
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
        movementValueNEgativ = movementValueNEgativ - 1f;
    }
}