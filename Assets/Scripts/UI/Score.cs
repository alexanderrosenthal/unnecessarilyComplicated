using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("LevelSetup")]
    public GameObject textElement;

    [Header("By Code")]
    public int countOfSattPigsTotal;

    //------------------- BASICS ---------------------------------------------------------
    private void Update()
    {
        string scoreDisplayed = countOfSattPigsTotal + " saturated pigs";

        textElement.GetComponent<TMP_Text>().text = scoreDisplayed;
    }
}