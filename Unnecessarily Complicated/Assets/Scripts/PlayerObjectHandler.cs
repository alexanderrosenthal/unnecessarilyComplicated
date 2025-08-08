using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectHandler : MonoBehaviour
{
    public List<GameObject> objectsOnTop = new List<GameObject>();

    public GameObject prefab;
    public Transform parent;
    public Transform sphere;


    public int objectsOnTopInt = 0;

    Collision thisCollision;
    CubeHandler cubeHandler;

    void OnCollisionEnter(Collision collision)
    {
        thisCollision = collision;

        if (collision.gameObject.name.Contains("CubeSpot"))
        {
            cubeHandler = collision.gameObject.transform.GetComponent<CubeHandler>();

            HandlePlayer();
        }
    }

    void HandlePlayer()
    {
        if (cubeHandler.takingInput)
        {
            PlayerGivingObject();
        }
        if (cubeHandler.givingOutput)
        {
            PlayerTakingObject();
        }
    }

    public void PlayerTakingObject()
    {
        Debug.Log("Player take Object");
        objectsOnTopInt = objectsOnTopInt + 1;

        HandleObjects(1);

        HandleCube(1);
    }

    public void PlayerGivingObject()
    {
        Debug.Log("Player give Object");

        if (objectsOnTopInt == 0)
        {
            Debug.Log("No Object on top");
        }
        else
        {
            objectsOnTopInt = objectsOnTopInt - 1;
            HandleObjects(-1);
        }

        HandleCube(-1);
    }

    void HandleObjects(int change)
    {
        if (change == 1)
        {
            PlaceRandomOnSphere();
        }
        else if (change == -1)
        {
            int random = Random.Range(0, objectsOnTop.Count);

            GameObject objectToDestroy = objectsOnTop[random];

            objectsOnTop.Remove(objectToDestroy);
            Destroy(objectToDestroy);
        }
    }
    void PlaceRandomOnSphere()
    {
        // Radius berechnen (bei gleichmäßiger Skalierung)
        float radius = sphere.localScale.x * 0.5f;

        // Zufälliger Punkt auf einer Kugel
        Vector3 randomDir = Random.onUnitSphere; // Normierter Richtungsvektor

        // Weltposition auf der Außenseite der Kugel
        Vector3 spawnPos = sphere.position + randomDir * radius;

        // Prefab instanziieren und als Child setzen
        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity, sphere);

        // Optional: Objekt von der Kugel weg ausrichten
        obj.transform.rotation = Quaternion.LookRotation(randomDir);

        obj.transform.SetParent(parent);

        objectsOnTop.Add(obj);
    }

    void HandleCube(int change)
    {
        cubeHandler.HandleInputOnCube(change);
    }
}
