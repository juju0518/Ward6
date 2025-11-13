using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
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
        Debug.Log("ResetScene called"); 
        
        if (anomalyManager == null) 
        {
            RefreshManagers();
            if (anomalyManager == null) return;
        }

        anomalyManager.DeactivateAllAnomalies();
        
        if (forceNoAnomaly || (progressManager != null && progressManager.GetCurrentProgress() == 0))
        {
            Debug.Log("no anomaly spawned"); 
            return;
        }

        Debug.Log("attempting to spawn anomaly");
        anomalyManager.TrySpawnAnomaly();
    }

    public void PlayerGuess(bool foundAnomaly)
    {
        if (progressManager == null) 
        {
            RefreshManagers();
            if (progressManager == null) return;
        }

        Anomaly currentAnomaly = anomalyManager?.GetCurrentAnomaly();
        bool anomalyExists = currentAnomaly != null;
        bool correctGuess = false;
        
        if (anomalyExists)
        {

            if (foundAnomaly)
            {
                correctGuess = true;
                Debug.Log("CORRECT, anomaly existed");
            }
            else
            {
                correctGuess = false;
                Debug.Log("INCORRECT, anomaly existed and was ignored");
            }
        }
        else
        {
            if (!foundAnomaly)
            {
                correctGuess = true;
                Debug.Log("CORRECT, no anomaly");
            }
            else
            {
                correctGuess = false;
                Debug.Log("INCORRECT, no anomaly but walked backward");
            }
        }

        if (correctGuess)
        {
            progressManager.CorrectGuess();
            Debug.Log($"progress advanced to: {progressManager.GetCurrentProgress()}");
        }
        else
        {
            progressManager.IncorrectGuess();
            Debug.Log("progress reset to 0");
        }

        if (progressManager.IsAtMaxProgress())
        {
            Debug.Log("you won!");
            return;
        }
        
        ResetScene();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("[TEST] Y key");
            PlayerGuess(foundAnomaly: true);
        }
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("[TEST] N key");
            PlayerGuess(foundAnomaly: false);
        }
    }
}