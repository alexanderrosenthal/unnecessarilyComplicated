using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPigs : MonoBehaviour
{
    public GameObject prefab;   // Das Prefab, das instanziert werden soll   F
    public Transform parent;

    public Transform target;
    public float minSpawnInterval = 0.5f;  // minimale Wartezeit in Sekunden
    public float maxSpawnInterval = 2f;    // maximale Wartezeit in SekundenF

    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;
    public float yLevel = 0f; // Höhe in der Ebene

    void Start()
    {

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
        // Zufallsposition in der Ebene berechnen
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 randomPosition = new Vector3(randomX, 3.1f, randomZ);

        // Prefab an der zufälligen Position erzeugen (Rotation vom Parent übernehmen)
        GameObject instance = Instantiate(prefab, randomPosition, parent.rotation, parent);

        float randomXX = Random.Range(0f, 360f);
        float randomZZ = Random.Range(0f, 360f);

        // Y-Achse bleibt wie sie ist
        instance.transform.rotation = Quaternion.Euler(randomXX, instance.transform.eulerAngles.y, randomZZ);
    }
}
