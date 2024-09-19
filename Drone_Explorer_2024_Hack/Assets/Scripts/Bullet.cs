using UnityEngine;
using static Bullet;

public class Bullet : MonoBehaviour
{
    #region Events

    public delegate void BulletDestroyEnemy(int points);
    public static event BulletDestroyEnemy OnBulletDestroyEnemy;

    #endregion

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
            OnBulletDestroyEnemy?.Invoke(5);
            Debug.Log("Hit the bad guy");
        }

        Destroy(gameObject);
    }
}

