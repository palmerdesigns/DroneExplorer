using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;
    public float panningSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Application.isPlaying)
        {
            // Handle rotation
            float mouseX = Input.GetAxis("Mouse X") * panningSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0, mouseX, 0);
            rb.MoveRotation(rb.rotation * rotation);

            // Handle movement
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            Vector3 movement = transform.right * moveX + transform.forward * moveZ;
            rb.MovePosition(transform.position + movement);
        }
    }
}
