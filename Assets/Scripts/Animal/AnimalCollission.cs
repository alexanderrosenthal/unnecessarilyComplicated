using UnityEngine;

public class AnimalCollission : MonoBehaviour
{
    Collision thisCollision;

    void OnCollisionEnter(Collision collision)
    {
        thisCollision = collision;
        CubeHandler cubeHandler;

        if (collision.gameObject.name.Contains("CubeSpot"))
        {
            cubeHandler = collision.gameObject.transform.GetComponent<CubeHandler>();

            //HandlePlayer();
        }
    }
}
