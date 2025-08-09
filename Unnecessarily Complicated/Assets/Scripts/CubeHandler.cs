using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeHandler : MonoBehaviour
{
    public List<GameObject> objectsOnTop = new List<GameObject>();
    public bool givingOutput;
    public bool takingInput;
    public int maxCountOnCube = 5;
    public int countOnCube = 1;
    public int localChangeInCount = 1;

    [SerializeField] private Vector3 startScale = Vector3.one; // Unterschiedliche Startgrößen für X, Y, Z
    [SerializeField] private float scaleFactor = 0.9f;         // Gleicher Faktor für alle Achsen

    public GameObject prefab;
    public Transform parent;

    public List<GameObject> TextComponents;

    public float minInterval = 0.1f;  // minimale Wartezeit in Sekunden
    public float maxInterval = 0.2f;    // maximale Wartezeit in Sekunden

    private void Start()
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

        StartCoroutine(TriggerRoutine());
    }

    //WACHSTUM
    IEnumerator TriggerRoutine()
    {
        while (true)
        {
            // Warte zufällige Zeit
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // Aktion ausführen
            TriggerAction();
        }
    }

    void TriggerAction()
    {
        if (givingOutput && countOnCube < maxCountOnCube)
        {
            HandleInputOnCubePig(-1, false);
        }
    }

    //ÜBER PLAYER
    public void HandleInputOnCubePlayer(int changeInCount, bool figureOnIT)
    {
        localChangeInCount = changeInCount;

        if (takingInput)
        {
            IncreaseFood();
        }
        if (givingOutput)
        {
            DecreaseFood(changeInCount);
        }
    }

    public void HandleInputOnCubePig(int changeInCount, bool figureOnIT)
    {
        localChangeInCount = changeInCount;

        if (takingInput)
        {
            DecreaseFood(changeInCount);
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
                    DecreaseFood(changeInCount);
                }
            }
        }
    }

    private void DecreaseFood(int changeInCount)
    {
        countOnCube = countOnCube - changeInCount;

        GameObject objectToDestroy = objectsOnTop[objectsOnTop.Count - 1];

        objectsOnTop.Remove(objectToDestroy);
        Destroy(objectToDestroy);
    }

    private void IncreaseFood()
    {
        countOnCube = countOnCube - localChangeInCount;

        GameObject obj = Instantiate(prefab, parent);

        // Optional: Lokale Position/Rotation zurücksetzen
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        float up = 2f + (0.5f * countOnCube - 0.5f);

        obj.transform.position += new Vector3(0f, up, 0f);

        objectsOnTop.Add(obj);
    }


    //TEXT
    private void Update()
    {
        HandleTreeSize();
        HandleText();
    }

    void HandleText()
    {
        string textToChange = countOnCube.ToString();

        for (int i = 0; i < 4; i++)
        {
            TextComponents[i].GetComponent<TMP_Text>().text = textToChange;
        }
    }
    void HandleTreeSize()
    {
        for (int i = 0; i < objectsOnTop.Count; i++)
        {
            float factor = Mathf.Pow(scaleFactor, i);
            objectsOnTop[i].transform.localScale = startScale * factor;
        }
    }
}
