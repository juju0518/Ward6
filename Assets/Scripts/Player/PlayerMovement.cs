using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Transform orientation;
    [SerializeField] private float drag = 6f;

    [SerializeField] private AudioSource footstepsSound;
    
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        MyInput();
        rb.linearDamping = drag;
        if (MyInput())
        {
            footstepsSound.enabled = true;
        }else
        {
            footstepsSound.enabled = false;
        }
        
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private bool MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (horizontalInput != 0|| verticalInput != 0)
        {
            return true;
        }
        return false;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        //rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.deltaTime);
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}