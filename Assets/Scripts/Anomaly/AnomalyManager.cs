using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AnomalyManager : MonoBehaviour
{// delete debug logs post testing !!!
    [SerializeField] private float anomalyChance = 0.6f; // 60% chance of an anomaly
    [SerializeField] private float obviousnessChance = 0.2f; // 20% obvious 80% not 
    private List<Anomaly> allAnomalies; 
    private List<Anomaly> availableAnomalies;
    private Anomaly currentAnomaly;

    void Awake()
    {
        allAnomalies = FindObjectsOfType<Anomaly>().ToList();
        Debug.Log($"found {allAnomalies.Count} anomalies in scene");
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
            Debug.LogWarning("No anomalies available!");
            return false;
        }

        if (!forceSpawn && Random.value > anomalyChance)
        {
            Debug.Log("no anomaly this scene");
            return false;
        }

        if (availableAnomalies.Count == 0)
        {
            Debug.Log("refilling anomalies");
            RefillAvailableAnomalies();
        }

        bool wantObvious = Random.value < obviousnessChance;
        Obviousness targetObviousness = wantObvious ? Obviousness.Obvious : Obviousness.NotObvious;

        var potentialAnomalies = availableAnomalies
            .Where(a => a != null && a.obviousness == targetObviousness).ToList();

        if (potentialAnomalies.Count == 0)
        {
            potentialAnomalies = availableAnomalies.Where(a => a != null).ToList();
            Debug.Log($"no {targetObviousness} anomalies available, using remaining");
        }

        if (potentialAnomalies.Count > 0)
        {
            int randomIndex = Random.Range(0, potentialAnomalies.Count);
            currentAnomaly = potentialAnomalies[randomIndex];
            
            availableAnomalies.Remove(currentAnomaly);
            currentAnomaly.SetActiveAnomaly(true);
            
            Debug.Log($"spawned anomaly: {currentAnomaly.name} ({currentAnomaly.obviousness}) - {availableAnomalies.Count} left");
            return true;
        }

        Debug.Log("UHOHJOno valid anomalies to spawn.");
        return false;
    }

    private void RefillAvailableAnomalies()
    {
        availableAnomalies = new List<Anomaly>(allAnomalies);
        Debug.Log("refilled anomalies");
    }

    public Anomaly GetCurrentAnomaly()
    {
        return currentAnomaly;
    }

}