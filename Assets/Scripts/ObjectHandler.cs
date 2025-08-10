using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    public List<GameObject> objectsOnTop = new List<GameObject>();
    public GameObject prefab;
    public GameObject particlePrefab; // Dein Particle System Prefab
    public Transform parent;
    public Transform sphere;
    public int objectsOnTopInt = 0;
    Collision thisCollision;
    CubeHandler cubeHandler;

    //------------------- INDIVIDUALS ---------------------------------------------------------
    void OnCollisionEnter(Collision collision)
    {
        thisCollision = collision;

        if (collision.gameObject.name.Contains("Ground Plate"))
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
        if (cubeHandler.givingOutput && cubeHandler.countOnCube > 0)
        {
            PlayerTakingObject();
        }
        else
        {
            Debug.Log("Empty");
        }
    }

    public void PlayerTakingObject()
    {
        //Debug.Log("Player take Object");
        objectsOnTopInt = objectsOnTopInt + 1;

        HandleObjects(1);

        HandleCube(1);
    }

    public void PlayerGivingObject()
    {
        if (objectsOnTopInt == 0)
        {
            Debug.Log("No Object on top");
        }
        else
        {
            if (cubeHandler.objectsOnTop.Count <= cubeHandler.maxCountOnCube)
            {
                //Debug.Log("Player give Object");

                objectsOnTopInt = objectsOnTopInt - 1;
                HandleObjects(-1);
                HandleCube(-1);

            }
            else
            {
                Debug.Log("Stock full");
            }
        }
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
            SpawnParticle(objectToDestroy);
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

        SpawnParticle(obj);
    }

    void HandleCube(int change)
    {
        cubeHandler.HandleInputOnCubePlayer(change, true);
    }

    public void SpawnParticle(GameObject objectToDestroy)
    {
        Vector3 position = objectToDestroy.transform.position;
        Quaternion rotation = Quaternion.identity;

        GameObject particleInstance = Instantiate(particlePrefab, position, rotation);

        // Falls es ein ParticleSystem hat → Dauer berechnen und danach zerstören
        ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            float totalDuration = ps.main.duration + ps.main.startLifetime.constantMax;
            Destroy(particleInstance, totalDuration);
        }
        else
        {
            // Falls kein ParticleSystem dran ist → Sicherheitszerstörung nach 2s
            Destroy(particleInstance, 2f);
        }
    }
}