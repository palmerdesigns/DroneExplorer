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
        Destroy(gameObject);
    }
}

