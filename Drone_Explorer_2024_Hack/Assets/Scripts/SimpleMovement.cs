using UnityEngine;

[ExecuteInEditMode]
public class SimpleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;
    public float panningSpeed = 100f;

    void Start()
    {
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Application.isPlaying)
        {
            // Handle movement
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            transform.Translate(moveX, 0, moveZ);

            // Handle rotation
            float mouseX = Input.GetAxis("Mouse X") * panningSpeed * Time.deltaTime;
            transform.Rotate(0, mouseX, 0);
        }
    }
}
