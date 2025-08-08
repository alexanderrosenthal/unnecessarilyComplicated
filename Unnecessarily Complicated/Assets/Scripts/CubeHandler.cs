using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeHandler : MonoBehaviour
{
    public List<GameObject> objectsOnTop = new List<GameObject>();
    public bool givingOutput;
    public bool takingInput;
    public int countOnCube = 1;
    public int changeInCount = 1;

    public GameObject prefab;
    public Transform parent;

    public List<GameObject> TextComponents;

    private void Start()
    {
        for (int i = 0; i < countOnCube; i++)
        {
            // Prefab instanziieren
            GameObject obj = Instantiate(prefab, parent);

            // Optional: Lokale Position/Rotation zurücksetzen
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            float up = 2.3f + (0.5f * i);

            obj.transform.position += new Vector3(0f, up, 0f);

            objectsOnTop.Add(obj);
        }
    }

    private void Update()
    {
        HandleText();
    }

    public void HandleInputOnCube(int changeInCount)
    {
        changeInCount = changeInCount;

        if (takingInput)
        {
            countOnCube = countOnCube - changeInCount;

            GameObject obj = Instantiate(prefab, parent);

            // Optional: Lokale Position/Rotation zurücksetzen
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            float up = 2.3f + (0.5f * countOnCube - 0.5f);

            obj.transform.position += new Vector3(0f, up, 0f);

            objectsOnTop.Add(obj);
        }
        if (givingOutput)
        {
            if (countOnCube == 0)
            {
                Debug.Log("Cube ist 0 -->" + countOnCube);
            }
            else
            {
                countOnCube = countOnCube - changeInCount;

                GameObject objectToDestroy = objectsOnTop[objectsOnTop.Count - 1];

                objectsOnTop.Remove(objectToDestroy);
                Destroy(objectToDestroy);
            }
        }
    }

    void HandleText()
    {
        string textToChange = countOnCube.ToString();

        for (int i = 0; i < 4; i++)
        {
            TextComponents[i].GetComponent<TMP_Text>().text = textToChange;
        }
    }
}
