using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform cameraPosition;

    void Update()
    {
        transform.position = cameraPosition.position;
        
    }
}
