using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeHandler : MonoBehaviour
{
    private int countOnCube = 1;

    public List<GameObject> TextComponents;

    void OnCollisionEnter(Collision collision)
    {
        countOnCube = countOnCube + 1;
        Debug.Log("hit" + countOnCube);

        string textToChange = countOnCube.ToString();

        for (int i = 0; i < 4; i++)
        {
            TextComponents[i].GetComponent<TMP_Text>().text = textToChange;
        }
    }
}
