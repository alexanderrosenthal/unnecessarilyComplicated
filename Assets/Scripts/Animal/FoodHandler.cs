using UnityEngine;

public class FoodHandler : MonoBehaviour
{
    [Header("GameDesign")]
    public int objectsEatenInt = 0;

    [Header("By Code")]
    public bool satt;

    //PRIVATE
    private SpotHandler cubeHandler;
    private bool colliding = false;

    //------------------- BASICS ---------------------------------------------------------
    void Update()
    {
        if (objectsEatenInt > 1)
        {
            satt = true;
            transform.GetComponent<AnimalMovement>().onTheWayTowards = false;
        }

        if (colliding)
        {
            if (cubeHandler.countOnCube > 0 && !satt)
            {
                AnimalTakingObject();
            }
        }
    }

    //------------------- INDIVIDUAL ---------------------------------------------------------
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Spot"))
        {
            //Debug.Log("Animal collission");
            colliding = true;

            cubeHandler = collision.gameObject.transform.GetComponent<SpotHandler>();
        }
    }

    public void AnimalTakingObject()
    {
        objectsEatenInt++;

        HandleCube(1);
    }

    void HandleCube(int change)
    {
        cubeHandler.ChangeByPig(change, true);
    }
}