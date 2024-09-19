using UnityEngine;

public class Bullet : MonoBehaviour
{

    void Update()
    {
        //Assures the bullet is destroyed after 3 seconds
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        //TO DO: Adding this as a stop gap to destroy enemy on collision
        if (hit.CompareTag("BadGuy"))
        {
            Destroy(hit);
            Debug.Log("Hit the bad guy");
        }

        Destroy(gameObject);
    }

    
}

