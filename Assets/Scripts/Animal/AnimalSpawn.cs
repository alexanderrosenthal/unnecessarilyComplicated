using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AnimalSpawn : MonoBehaviour
{
    public GameObject prefab;   // Das Prefab, das instanziert werden soll
    public Transform parent;    // Das Parent-Objekt, unter dem das neue Objekt liegen soll

    public int maxPigs = 5;

    public int maxPigsCount = 0;

    public Transform spawnParent;
    public List<Transform> spawnPoints = new List<Transform>();


    public float minSpawnInterval = 0.5f;  // minimale Wartezeit in Sekunden
    public float maxSpawnInterval = 2f;    // maximale Wartezeit in Sekunden

    void Start()
    {
        //Stellt Liste der Spawnpoints zusammen
        for (int i = 0; i < spawnParent.childCount; i++)
        {
            spawnPoints.Add(spawnParent.GetChild(i).transform);
        }

        StartCoroutine(TriggerRoutine());
    }

    IEnumerator TriggerRoutine()
    {
        while (true)
        {
            // Warte zufällige Zeit
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            // Aktion ausführen
            TriggerAction();
        }
    }

    void TriggerAction()
    {
        if (maxPigsCount > maxPigs)
        {
            Debug.Log("no more pigs");
            return;
        }


        maxPigsCount = maxPigsCount + 1;

        //Debug.Log("maxPigsCount " + maxPigsCount);

        int random = Random.Range(0, spawnPoints.Count);

        parent = spawnPoints[random];

        if (prefab != null && parent != null)
        {
            // Instanziere das Prefab an der Position und Rotation des Parents
            GameObject instance = Instantiate(prefab, parent.position, parent.rotation);

            // Setze das Parent-Transform des Instanzierten Objekts
            instance.transform.SetParent(parent);

            Debug.LogWarning("Vector3.zero + " + Vector3.zero);
            Debug.LogWarning("Quaternion.identity + " + Quaternion.identity);
            Debug.LogWarning("Vector3.one + " + Vector3.one);

            // Optional: Falls du die lokale Position/Rotation/Skalierung zurücksetzen möchtest:
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
            instance.transform.localScale = Vector3.one;

            instance.transform.GetChild(0).GetComponent<AnimalMovement>().home = parent.GetChild(1);

        }
        else
        {
            Debug.LogWarning("Prefab oder Parent nicht gesetzt!");
        }
    }
}