using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;
    public float panningSpeed = 100f;
    Projectiles projectiles;
    Menus menu;

    private Rigidbody rb;

    void Start()
    {
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Get the Projectiles component
        projectiles = GetComponent<Projectiles>();
        menu = FindObjectOfType<Menus>();
    }

    void Update()
    {
        // Handle pause toggle
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed.");
            menu.TogglePause();
        }

        if (Application.isPlaying && !menu.isPaused)
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

            // Handle Shooting
            if (Input.GetMouseButtonDown(0) && Time.time > projectiles.nextFireTime)
            {
                projectiles.nextFireTime = Time.time + projectiles.fireRate;
                projectiles.Shoot();
            }
        }
    }

    public void UpdateCursorState()
    {
        if (menu.isPaused)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
