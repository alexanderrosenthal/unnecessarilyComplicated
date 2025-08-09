using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public List<GameObject> Spots = new List<GameObject>();

    Transform buschParent;
    public float moveSpeed = 5f;  // Geschwindigkeit in Einheiten pro Sekunde

    float movementValue = 1f;
    float movementValueNEgativ = -1f;

    // Start is called before the first frame update
    void Start()
    {
        buschParent = GameObject.Find("Spots").transform;

        //Stellt Liste der Spawnpoints zusammen
        for (int i = 0; i < buschParent.childCount; i++)
        {
            Spots.Add(buschParent.GetChild(i).gameObject);
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
        for (int i = 0; i < Spots.Count; i++)
        {
            float RandomX = Random.Range(movementValueNEgativ, movementValue);
            float RandomZ = Random.Range(movementValueNEgativ, movementValue);

            Spots[i].transform.position = Spots[i].transform.position + new Vector3(RandomX, 0, RandomZ);
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
