using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private AnomalyManager anomalyManager;
    private ProgressManager progressManager;

    void Awake()
    {
        anomalyManager = FindObjectOfType<AnomalyManager>();
        progressManager = FindObjectOfType<ProgressManager>();
    }

    void Start()
    {
        ResetScene(forceNoAnomaly: true); 
    }

    public void ResetScene(bool forceNoAnomaly = false)
    {
        Debug.Log("ResetScene called"); 
        
        if (anomalyManager == null) return;

        anomalyManager.DeactivateAllAnomalies();
        
        if (forceNoAnomaly || (progressManager != null && progressManager.GetCurrentProgress() == 0))
        {
            Debug.Log("No anomaly spawned"); 
            return;
        }

        Debug.Log("attempting to spawn anomaly");
        anomalyManager.TrySpawnAnomaly();
    }

    public void PlayerGuess(bool foundAnomaly)
    {
        if (progressManager == null) return;

        Anomaly currentAnomaly = anomalyManager.GetCurrentAnomaly();
        bool anomalyExists = currentAnomaly != null;

        bool correctGuess = (foundAnomaly && anomalyExists) || (!foundAnomaly && !anomalyExists);

        if (correctGuess)
        {
            progressManager.CorrectGuess();
            Debug.Log("correct guess!");
        }
        else
        {
            progressManager.IncorrectGuess();
            Debug.Log("incorrect guess!");
        }

        if (progressManager.IsAtMaxProgress())
        {
            Debug.Log("u won!");
            return;
        }
        
        Debug.Log("CALLING RESETSCENE AFTER GUESS");
        ResetScene();
    }
    
    void Update() // FOR TESTING PURPOSES - DELETE LATER
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Debug.Log("reset scene!!");
            ResetScene();
        }
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            Debug.Log("Y key pressed - guess ANOMALY");
            PlayerGuess(foundAnomaly: true);
        }
        
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            Debug.Log("N key pressed - guess NO ANOMALY");
            PlayerGuess(foundAnomaly: false);
        }
    }
}