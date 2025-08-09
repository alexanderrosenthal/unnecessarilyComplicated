using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public GameObject textElement;
    public int countOfSattPigsTotal;

    private void Update()
    {
        string scoreDisplayed = countOfSattPigsTotal + " saturated pigs";

        textElement.GetComponent<TMP_Text>().text = scoreDisplayed;
    }
}
