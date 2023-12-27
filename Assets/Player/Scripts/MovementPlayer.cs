using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    private PlayerControls m_playerControls;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump;

    [SerializeField] private readonly float m_mouseSensitivity = 10.0f;
    private readonly float m_upDownRange = 55.0f;
    float rotY = 0;
    float rotX = 0;
    Rigidbody rb;

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;

    [Header("Movement Parameters")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    CursorLockMode wantedMode;
    

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float jumpHeight = 10.0f;
       

    

    public ParticleSystem muzzleFlash;
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        HandleJump();

    }
    

    // Update is called once per frame
    void Update()
    {

        // sprinting
        if (IsSprinting)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = 5f;
        }
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
        // TODO: MAKE Jumping not vary in height based on movement before jump
            ShouldJump = Input.GetKey(jumpKey) && rb.velocity.y == 0;

        // Code to maybe fix cursor not being there when shop opens
        if (Input.GetKeyDown(KeyCode.B)){
            wantedMode = CursorLockMode.None;
        }
        
    }

    private void HandleJump()
    {
        if (ShouldJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
    
    void SetWantedMode(CursorLockMode wantedMode)
    {
        wantedMode = CursorLockMode.Locked;
    }
}
