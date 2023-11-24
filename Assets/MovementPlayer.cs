using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    private PlayerControls m_playerControls;
    [SerializeField] private readonly float m_mouseSensitivity = 10.0f;

    private readonly float m_upDownRange = 55.0f;
    float rotY = 0;
    float rotX = 0;
    [SerializeField] private float speed = 5f;
    CursorLockMode wantedMode;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {

        m_playerControls = new PlayerControls();
        m_playerControls.DefaultInput.onPress.performed += DoStuff;

        wantedMode = CursorLockMode.Locked;

    }

    void FixedUpdate()
    {
        FPSMouseLook();
        SetCursorState();
    }

    // Update is called once per frame
    void Update()
    {


        // W movement
        if (Keyboard.current != null && Keyboard.current.wKey.isPressed)
        {
            //Debug.Log("W was pressed!");
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        // A movement
        if (Keyboard.current != null && Keyboard.current.aKey.isPressed)
        {
            //Debug.Log("A was pressed!");
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        // S movement
        if (Keyboard.current != null && Keyboard.current.sKey.isPressed)
        {
            //Debug.Log("S was pressed!");
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

        // D movement
        if (Keyboard.current != null && Keyboard.current.dKey.isPressed)
        {
            //Debug.Log("D was pressed!");
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        // escape locked cursor
        if (Keyboard.current != null && Keyboard.current.escapeKey.isPressed)
        {
            wantedMode = CursorLockMode.None;
        }
        // left click to shoot
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;

            // ray from center of screen
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));


            if (Physics.Raycast(ray, out hit))
            {

                if (hit.transform.tag == "Enemy")
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log("Hit");
                    //Destroy(hit.transform.gameObject);

                    hit.transform.GetComponent<Renderer>().material.color = Color.black;
                }
                Debug.DrawLine(ray.origin, hit.point, Color.red, 1);
            }else{
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("No Hit");
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

    void DoStuff(InputAction.CallbackContext context)
    {
        Debug.Log("DoStuff called!");
    }

    private void FPSMouseLook()
    {

        rotY -= Input.GetAxis("Mouse Y") * m_mouseSensitivity;
        rotX = Input.GetAxis("Mouse X") * m_mouseSensitivity;

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
