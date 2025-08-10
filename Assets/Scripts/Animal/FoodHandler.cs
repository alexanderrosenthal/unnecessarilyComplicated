using UnityEngine;

public class FoodHandler : MonoBehaviour
{
    public int objectsEatenInt = 0;
    Collision thisCollision;
    CubeHandler cubeHandler;
    bool colliding = false;
    public bool satt;

    //------------------- BASICS ---------------------------------------------------------
    void Update()
    {
        if (objectsEatenInt > 1)
        {
            satt = true;
            transform.GetChild(0).GetComponent<AnimalMovement>().onTheWayTowards = false;
        }

        if (colliding && cubeHandler.countOnCube > 0 && !satt)
        {
            AnimalTakingObject();
        }
    }

    //------------------- INDIVIDUAL ---------------------------------------------------------
    void OnCollisionEnter(Collision collision)
    {
        thisCollision = collision;

        if (collision.gameObject.name.Contains("Ground Plate"))
        {
            //Debug.Log("Animal collission");
            colliding = true;

            cubeHandler = collision.gameObject.transform.GetComponent<CubeHandler>();
        }
    }

    public void AnimalTakingObject()
    {
        objectsEatenInt = objectsEatenInt + 1;

        HandleCube(1);
    }

    void HandleCube(int change)
    {
        cubeHandler.HandleInputOnCubePig(change, true);
    }
}