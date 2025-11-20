using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AnomalyManager : MonoBehaviour
{
    [SerializeField] private float anomalyChance;
    [SerializeField] private float obviousnessChance;
    private static List<Anomaly> allAnomalies; 
    private List<Anomaly> availableAnomalies;
    private Anomaly currentAnomaly;

    void Awake()
    {
        allAnomalies = FindObjectsOfType<Anomaly>().ToList();
        RefillAvailableAnomalies();
    }

    public void DeactivateAllAnomalies()
    {
        foreach (var anomaly in allAnomalies.Where(a => a != null))
        {
            anomaly.SetActiveAnomaly(false);
        }
        currentAnomaly = null;
    }

    public bool TrySpawnAnomaly(bool forceSpawn = false)
    {
        if (allAnomalies == null || allAnomalies.Count == 0)
        {
            return false;
        }

        if (!forceSpawn && Random.value > anomalyChance)
        {
            return false;
        }

        if (availableAnomalies.Count == 0)
        {
            RefillAvailableAnomalies();
        }

        bool wantObvious = Random.value < obviousnessChance;
        Obviousness targetObviousness = wantObvious ? Obviousness.Obvious : Obviousness.NotObvious;

        var potentialAnomalies = availableAnomalies
            .Where(a => a != null && a.obviousness == targetObviousness).ToList();

        if (potentialAnomalies.Count == 0)
        {
            potentialAnomalies = availableAnomalies.Where(a => a != null).ToList();
        }

        if (potentialAnomalies.Count > 0)
        {
            int randomIndex = Random.Range(0, potentialAnomalies.Count);
            currentAnomaly = potentialAnomalies[randomIndex];
            
            availableAnomalies.Remove(currentAnomaly);
            currentAnomaly.SetActiveAnomaly(true);
            
            return true;
        }

        return false;
    }

    private void RefillAvailableAnomalies()
    {
        availableAnomalies = new List<Anomaly>(allAnomalies);
    }

    public Anomaly GetCurrentAnomaly()
    {
        return currentAnomaly;
    }
}