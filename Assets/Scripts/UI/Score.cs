using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public GameObject textElement;
    public int countOfSattPigsTotal;

    //------------------- BASICS ---------------------------------------------------------
    private void Update()
    {
        string scoreDisplayed = countOfSattPigsTotal + " saturated pigs";

        textElement.GetComponent<TMP_Text>().text = scoreDisplayed;
    }
}