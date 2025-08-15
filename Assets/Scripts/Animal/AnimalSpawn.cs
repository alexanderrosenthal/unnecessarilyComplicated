using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AnimalSpawn : MonoBehaviour
{
    [Header("GameDesign")]
    public int maxPigs = 5;
    public int maxPigsCount = 0;
    public float minSpawnInterval = 0.5f;  // minimale Wartezeit in Sekunden
    public float maxSpawnInterval = 2f;    // maximale Wartezeit in Sekunden

    [Header("LevelSetup")]
    public GameObject prefab;   // Das Prefab, das instanziert werden soll
    public Transform animalParent;    // Das Parent-Objekt, unter dem das neue Objekt liegen soll
    private Transform spawnPosition;

    [Header("By Code")]
    public Transform spawnParent; public List<Transform> listOfSpawns = new List<Transform>();

    //------------------- BASICS ---------------------------------------------------------
    void Start()
    {
        //Stellt Liste der Spawnpoints zusammen
        for (int i = 0; i < transform.childCount; i++)
        {
            listOfSpawns.Add(transform.GetChild(i).transform);
        }

        StartCoroutine(TriggerRoutine());
    }

    //------------------- INDIVIDUALS ---------------------------------------------------------
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
        HandleMaxPigs();

        ChooseSpawn();

        if (prefab != null && animalParent != null)
        {
            // Instanziere das Prefab an der Position und Rotation des Parents
            GameObject instance = Instantiate(prefab, spawnPosition.position, spawnPosition.rotation);

            // Setze das Parent-Transform des Instanzierten Objekts
            instance.transform.SetParent(animalParent.GetChild(0));

            instance.transform.GetComponent<AnimalMovement>().home = spawnPosition;
        }
        else
        {
            Debug.LogWarning("Prefab oder Parent nicht gesetzt!");
        }
    }

    private bool HandleMaxPigs()
    {
        //Sicherstellen, dass nur begrenzte Anzahl an Tieren
        if (maxPigsCount >= maxPigs)
        {
            return false;
        }

        maxPigsCount = maxPigsCount + 1;

        return true;
    }
    
    private void ChooseSpawn()
    {
        int random = Random.Range(0, listOfSpawns.Count - 1);

        spawnPosition = listOfSpawns[random];
    }
}