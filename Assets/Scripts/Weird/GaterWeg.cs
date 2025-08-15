using UnityEngine;

public class GaterWeg : MonoBehaviour
{
    [Header("LevelSetup")]
    public Transform gaterParent;

    //------------------- BASICS ---------------------------------------------------------
    void Start()
    {
        gaterParent = GameObject.Find("Fences all").transform;

        //Stellt Liste der Spawnpoints zusammen
        for (int i = 0; i < gaterParent.childCount; i++)
        {
            gaterParent.GetChild(i).gameObject.SetActive(false);
        }
    }
}