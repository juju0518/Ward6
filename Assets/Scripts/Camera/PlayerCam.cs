using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float senX;
    public float senY;
    public Transform orientation;

    private float xRotation;
    private float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Screen.lockCursor = true;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * senX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * senY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0); 
    }
}