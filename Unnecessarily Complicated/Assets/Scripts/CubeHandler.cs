using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeHandler : MonoBehaviour
{
    public bool givingOutput;
    public bool takingInput;
    public int countOnCube = 1;
    public int changeInCount = 1;

    public List<GameObject> TextComponents;

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
