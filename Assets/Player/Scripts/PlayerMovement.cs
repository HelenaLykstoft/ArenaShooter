using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls m_playerControls;
    [SerializeField] private float speed = 5f;
    private GameObject mainCamera;
    [SerializeField] public float sensitivity = 2.0f; // Adjust the sensitivity of mouse movement

    private readonly float m_upDownRange = 55.0f;
    float rotY = 0;
    float rotX = 0;
    CursorLockMode wantedMode;

    void FixedUpdate(){
        CameraMovement();
        SetCursorState();
    }

    // Update is called once per frame
    void Update()
    {
        //CameraMovement();
        //Debug.Log("Camera rotation: " + mainCamera.transform.rotation.eulerAngles);

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;

            // ray from center of screen
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit))
            {
                // Debug.Log(hit.transform.name);
                if (hit.transform.tag == "Enemy")
                {
                    //Destroy(hit.transform.gameObject);
                    hit.transform.GetComponent<Renderer>().material.color = Color.black;
                }
            }
        }


        // W movement

        if (Keyboard.current != null && Keyboard.current.wKey.isPressed)
        {
            Debug.Log("W was pressed!");
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        // A movement
        if (Keyboard.current != null && Keyboard.current.aKey.isPressed)
        {
            Debug.Log("A was pressed!");
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        // S movement
        if (Keyboard.current != null && Keyboard.current.sKey.isPressed)
        {
            Debug.Log("S was pressed!");
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

        // D movement
        if (Keyboard.current != null && Keyboard.current.dKey.isPressed)
        {
            Debug.Log("D was pressed!");
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("esc was pressed!");
            wantedMode = CursorLockMode.None;
        }

        if (Pointer.current != null)
        {
            if (Pointer.current.press.wasPressedThisFrame)
            {
                var cam = Camera.main;

                Debug.Log("Pointer pressed at: " + Pointer.current.position.ReadValue());

                var ray = cam.ScreenPointToRay(Pointer.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }

    }

    void OnEnable()
    {
        m_playerControls.DefaultInput.Enable();
    }


    void OnDisable()
    {
        m_playerControls.DefaultInput.Disable();
    }

    void Awake()
    {
        mainCamera = GameObject.Find("MainCamera");
        //mainCamera = GetComponent<MainCamera>();
        m_playerControls = new PlayerControls();
        m_playerControls.DefaultInput.onPress.performed += DoStuff;
        wantedMode = CursorLockMode.Locked;
    }


    void DoStuff(InputAction.CallbackContext context)
    {
        Debug.Log("DoStuff called!");
    }

    void CameraMovement()
    {
        /*float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the camera based on mouse movement
        transform.Rotate(Vector3, mouseX * sensitivity);
        transform.Rotate(Vector3.left, mouseY * sensitivity);

        // Clamp the vertical rotation to prevent flipping
        float xRotation = transform.eulerAngles.x;
        if (xRotation > 180.0f)
            xRotation -= 360.0f;
            xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        // Apply the clamped rotation
        transform.rotation = Quaternion.Euler(xRotation, transform.eulerAngles.y, 0.0f);
*/
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotX = Input.GetAxis("Mouse X") * sensitivity;

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            transform.Rotate(0, rotX, 0);

            rotY = Mathf.Clamp(rotY, -m_upDownRange, m_upDownRange);
            Camera.main.transform.localRotation = Quaternion.Euler(rotY, 0, 0);
        }
    }

     void SetCursorState()
    {
        Cursor.lockState = wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }
}
