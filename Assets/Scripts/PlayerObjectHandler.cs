using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectHandler : MonoBehaviour
{
    [Header("GameDesign")]
    public int objectsOnTopInt = 0;

    [Header("PrefabSetup")]
    public GameObject prefab;
    public GameObject particlePrefab; // Dein Particle System Prefab
    public List<GameObject> objectsOnTop = new();

    //PRIVATE
    private Transform parent;
    private Transform sphere;
    private SpotHandler spotHandler;

    //------------------- INDIVIDUALS ---------------------------------------------------------
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Spot"))
        {
            spotHandler = collision.transform.GetComponent<SpotHandler>();

            HandlePlayer();
        }
    }

    void HandlePlayer()
    {
        if (spotHandler.takingInput)
        {
            PlayerGivingObject();
        }
        if (spotHandler.givingOutput && spotHandler.countOnCube > 0)
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
        objectsOnTopInt++;

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
            if (spotHandler.objectsOnTop.Count <= spotHandler.maxCountOnCube)
            {
                //Debug.Log("Player give Object");

                objectsOnTopInt--;
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

    void HandleCube(int valueOfChange)
    {
        spotHandler.ChangeByPlayer(valueOfChange);
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