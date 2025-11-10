using UnityEngine;

public class TeleportArch : MonoBehaviour
{
    public Transform otherArch;
    public Transform thisArch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 localOffset = thisArch.InverseTransformPoint(other.transform.position);

            Quaternion rotationDiff = otherArch.rotation * Quaternion.Inverse(thisArch.rotation);
            Vector3 rotatedOffset = rotationDiff * localOffset;

            Vector3 targetPosition = otherArch.TransformPoint(rotatedOffset);

            other.transform.position = targetPosition;
        }
    }
}
