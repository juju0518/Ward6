using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform otherArch;
    public Transform thisArch;
    [SerializeField] private TeleportType teleportType;
    
    public enum TeleportType
    {
        Forward,
        Backward
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            Vector3 localOffset = thisArch.InverseTransformPoint(other.transform.position);
            Quaternion rotationDiff = otherArch.rotation * Quaternion.Inverse(thisArch.rotation);
            Vector3 rotatedOffset = rotationDiff * localOffset;
            Vector3 targetPosition = otherArch.TransformPoint(rotatedOffset);
            
            other.transform.position = targetPosition;

            if (GameManager.Instance != null)
            {
                bool foundAnomaly = (teleportType == TeleportType.Backward);
                GameManager.Instance.PlayerGuess(foundAnomaly);
            }
            else
            {
                Debug.LogWarning("GameManager instance not found.");
            }
        }
    }
}
