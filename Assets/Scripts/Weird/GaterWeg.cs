using UnityEngine;

public class GaterWeg : MonoBehaviour
{
    public Transform gaterParent;

    // Start is called before the first frame update
    void Start()
    {
        gaterParent = GameObject.Find("Gater_all").transform;

        //Stellt Liste der Spawnpoints zusammen
        for (int i = 0; i < gaterParent.childCount; i++)
        {
            gaterParent.GetChild(i).gameObject.SetActive(false);
        }
    }
}