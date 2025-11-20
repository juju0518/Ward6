using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action OnPlayerGuess;
    
    private AnomalyManager anomalyManager;
    private ProgressManager progressManager;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        RefreshManagers();
        ResetScene(forceNoAnomaly: true); 
    }
    
    public void RefreshManagers()
    {
        anomalyManager = FindObjectOfType<AnomalyManager>();
        progressManager = FindObjectOfType<ProgressManager>();
    }

    public void ResetScene(bool forceNoAnomaly = false)
    {   
        if (anomalyManager == null) 
        {
            RefreshManagers();
            if (anomalyManager == null) return;
        }

        anomalyManager.DeactivateAllAnomalies();
        
        if (forceNoAnomaly || (progressManager != null && progressManager.GetCurrentProgress() == 0))
        {
            return;
        }

        anomalyManager.TrySpawnAnomaly();
    }

    public void PlayerGuess(bool foundAnomaly)
    {
        if (progressManager == null) 
        {
            RefreshManagers();
            if (progressManager == null) return;
        }

        OnPlayerGuess?.Invoke();

        Anomaly currentAnomaly = anomalyManager?.GetCurrentAnomaly();
        bool anomalyExists = currentAnomaly != null;
        bool correctGuess = false;
        
        if (anomalyExists)
        {
            if (foundAnomaly)
            {
                correctGuess = true;
            }
            else
            {
                correctGuess = false;
            }
        }
        else
        {
            if (!foundAnomaly)
            {
                correctGuess = true;
            }
            else
            {
                correctGuess = false;
            }
        }

        if (correctGuess)
        {
            progressManager.CorrectGuess();
        }
        else
        {
            progressManager.IncorrectGuess();
        }

        ResetScene();
    }
    
    public bool IsAtMaxProgress()
    {
        return progressManager != null && progressManager.IsAtMaxProgress();
    }
    
    public bool IsCorrectFinalChoice(bool foundAnomaly)
    {
        Anomaly currentAnomaly = anomalyManager?.GetCurrentAnomaly();
        bool anomalyExists = currentAnomaly != null;
        
        return (foundAnomaly && anomalyExists) || (!foundAnomaly && !anomalyExists);
    }
    
    public void FailedFinalTest()
    {
        
        OnPlayerGuess?.Invoke();
        
        progressManager.IncorrectGuess();
        
        Teleport[] allTeleporters = FindObjectsOfType<Teleport>();
        foreach (Teleport teleporter in allTeleporters)
        {
            teleporter.ResetTeleporter();
        }
        
        ResetScene();
    }
}