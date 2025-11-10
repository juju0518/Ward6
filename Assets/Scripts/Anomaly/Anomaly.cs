using UnityEngine;

public enum Obviousness { NotObvious, Obvious }

public class Anomaly : MonoBehaviour
{
    public GameObject normalAppearance;
    public GameObject anomalyAppearance;
    public Obviousness obviousness;
    private bool isActiveAnomaly = false;

    public void SetActiveAnomaly(bool active)
    {
        isActiveAnomaly = active;
        UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        if (normalAppearance != null)
            normalAppearance.SetActive(!isActiveAnomaly);
        if (anomalyAppearance != null)
            anomalyAppearance.SetActive(isActiveAnomaly);
    }
}