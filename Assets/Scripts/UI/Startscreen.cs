using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startscreen : MonoBehaviour
{

    public Transform ScoreUI;
    public void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }


    public void deaktivierenStartUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < ScoreUI.childCount; i++)
        {
            ScoreUI.GetChild(i).gameObject.SetActive(true);
        }
    }
}
