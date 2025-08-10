using UnityEngine;

public class AnimalCollission : MonoBehaviour
{
    Collision thisCollision;

    //INDIVIDUAL
    void OnCollisionEnter(Collision collision)
    {
        thisCollision = collision;
        CubeHandler cubeHandler;

        if (collision.gameObject.name.Contains("Ground Plate"))
        {
            cubeHandler = collision.gameObject.transform.GetComponent<CubeHandler>();

            //HandlePlayer();
        }
    }
}
