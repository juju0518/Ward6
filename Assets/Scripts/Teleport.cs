using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform otherArch;
    public Transform thisArch;
    [SerializeField] private TeleportType teleportType;
    
    private bool isDisabled = false;
    
    public enum TeleportType
    {
        Forward,
        Backward
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDisabled) return;
        
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.IsAtMaxProgress())
                {
                    bool foundAnomaly = (teleportType == TeleportType.Backward);
                    bool isCorrectChoice = GameManager.Instance.IsCorrectFinalChoice(foundAnomaly);
                    
                    if (isCorrectChoice)
                    {
                        isDisabled = true;
                        return;
                    }
                    else
                    {
                        DoTeleport(other.transform);
                        GameManager.Instance.FailedFinalTest();
                        return;
                    }
                }
                else
                {
                    DoTeleport(other.transform);
                    bool foundAnomaly = (teleportType == TeleportType.Backward);
                    GameManager.Instance.PlayerGuess(foundAnomaly);
                }
            }
        }
    }
    
    private void DoTeleport(Transform player)
    {
        Vector3 localOffset = thisArch.InverseTransformPoint(player.position);
        Quaternion rotationDiff = otherArch.rotation * Quaternion.Inverse(thisArch.rotation);
        Vector3 rotatedOffset = rotationDiff * localOffset;
        Vector3 targetPosition = otherArch.TransformPoint(rotatedOffset);
        
        player.position = targetPosition;
    }
    
    public void ResetTeleporter()
    {
        isDisabled = false;
    }
}
