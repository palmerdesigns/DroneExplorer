using UnityEngine;

public class EnemyManager : MonoBehaviour
{
     private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                Debug.Log("Player entered enemy trigger");
                anim.SetTrigger("Blast");
                break;
        }
    }
}
