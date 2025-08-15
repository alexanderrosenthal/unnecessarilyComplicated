using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpotHandler : MonoBehaviour
{
    [Header("GameDesign")]
    public int maxCountOnCube = 5;
    public float minInterval = 0.1f;  // minimale Wartezeit in Sekunden
    public float maxInterval = 0.2f;    // maximale Wartezeit in Sekunden

    [Header("LevelSetup")]
    public bool givingOutput;
    public bool takingInput;

    [Header("PrefabSetup")]
    public GameObject prefab;
    public Transform parent;

    [Header("By Code")]
    public List<GameObject> objectsOnTop = new();
    public int countOnCube = 1;
    public int localChangeInCount = 1;

    //------------------- BASICS ---------------------------------------------------------
    private void Start()
    {
        PrefillWithFood();

        StartCoroutine(TriggerRoutineFoodSpawn());
    }
    
    //------------------- INDIVIDUALS ---------------------------------------------------------

    private void PrefillWithFood()
    {
        for (int i = 0; i < countOnCube; i++)
        {
            // Prefab instanziieren
            GameObject obj = Instantiate(prefab, parent);

            // Optional: Lokale Position/Rotation zurücksetzen
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            float up = 1.8f + (0.5f * i);

            obj.transform.position += new Vector3(0f, up, 0f);

            objectsOnTop.Add(obj);
        }
    }


    //------------------- WACHSTUM ---------------------------------------------------------
    IEnumerator TriggerRoutineFoodSpawn()
    {
        while (true)
        {
            // Warte zufällige Zeit
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // Aktion ausführen
            SpawnFood();
        }
    }

    void SpawnFood()
    {
        if (givingOutput && countOnCube < maxCountOnCube)
        {
            ChangeByPig(-1, false);
        }
    }

    //------------------- ÜBER PLAYER ---------------------------------------------------------
    public void ChangeByPlayer(int valueOfChange)
    {
        localChangeInCount = valueOfChange;

        if (takingInput)
        {
            IncreaseFood();
        }
        if (givingOutput)
        {
            DecreaseFood(valueOfChange);
        }
    }

    public void ChangeByPig(int valueOfChange, bool figureOnIT)
    {
        localChangeInCount = valueOfChange;

        if (takingInput)
        {
            DecreaseFood(valueOfChange);
        }
        if (givingOutput)
        {
            if (countOnCube == 0 && figureOnIT)
            {
                Debug.Log("Cube ist 0 -->" + countOnCube);
            }
            else
            {
                if (!figureOnIT)
                {
                    IncreaseFood();
                }
                else
                {
                    DecreaseFood(valueOfChange);
                }
            }
        }
    }

    private void DecreaseFood(int valueOfChange)
    {
        countOnCube -= valueOfChange;

        GameObject objectToDestroy = objectsOnTop[objectsOnTop.Count - 1];

        objectsOnTop.Remove(objectToDestroy);
        Destroy(objectToDestroy);
    }

    private void IncreaseFood()
    {
        countOnCube -= localChangeInCount;

        GameObject obj = Instantiate(prefab, parent);

        // Optional: Lokale Position/Rotation zurücksetzen
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        float up = 2f + (0.5f * countOnCube - 1f);

        obj.transform.position += new Vector3(0f, up, 0f);

        objectsOnTop.Add(obj);
    }
}