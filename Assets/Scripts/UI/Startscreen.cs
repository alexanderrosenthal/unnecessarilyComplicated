using UnityEngine;

public class Startscreen : MonoBehaviour
{
    [Header("LevelSetup")]
    public Transform ScoreUI;

    //------------------- BASICS ---------------------------------------------------------
    public void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    //------------------- INDIVIDUALS ---------------------------------------------------------
    public void deaktivierenStartUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < ScoreUI.childCount; i++)
        {
            ScoreUI.GetChild(i).gameObject.SetActive(true);
        }
    }
}