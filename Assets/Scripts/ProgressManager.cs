using UnityEngine;
using UnityEngine.Events;

public class ProgressManager : MonoBehaviour
{
    //[SerializeField] private GameObject[] clockPrefabs;
    //[SerializeField] private Transform clockParent;
    [SerializeField] private int maxProgress = 5;
    public UnityEvent<int> OnProgressChanged;
    public UnityEvent OnProgressReset;
    public UnityEvent OnMaxProgressReached;
    private int currentProgress = 0;
    //private GameObject currentClockInstance;

    void Start()
    {
        InitializeClock();
    }

    public void CorrectGuess()
    {
        currentProgress++;
        Debug.Log($"Correct! Progress: {currentProgress}");
        
        OnProgressChanged?.Invoke(currentProgress);
        //UpdateClock();
        
        if (currentProgress >= maxProgress)
        {
            Debug.Log("Max progress reached!");
            OnMaxProgressReached?.Invoke();
        }
    }

    public void IncorrectGuess()
    {
        Debug.Log($"Incorrect! Progress reset from {currentProgress} to 0");
        currentProgress = 0;
        
        OnProgressReset?.Invoke();
        OnProgressChanged?.Invoke(currentProgress);
        //UpdateClock();
    }

    private void InitializeClock()
    {
        //UpdateClock();
    }

    private void UpdateClock()
    {
        // Destroy current clock if it exists
        //if (currentClockInstance != null)
        //{
            //DestroyImmediate(currentClockInstance);
        //}

        // Spawn new clock based on progress
        //if (clockPrefabs != null && currentProgress < clockPrefabs.Length && clockPrefabs[currentProgress] != null)
        //{
            //Vector3 spawnPosition = clockParent != null ? clockParent.position : Vector3.zero;
            //Quaternion spawnRotation = clockParent != null ? clockParent.rotation : Quaternion.identity;
            
            //currentClockInstance = Instantiate(clockPrefabs[currentProgress], spawnPosition, spawnRotation);
            
            //if (clockParent != null)
            //{
                //currentClockInstance.transform.SetParent(clockParent, false);
            //}
            
            Debug.Log($"Clock updated to: {GetTimeString()}");
        //}
        //else
        //{
            Debug.LogWarning($"No clock prefab available for progress level {currentProgress}");
        //}
    }

    private string GetTimeString()
    {
        if (currentProgress == 0) return "Midnight";
        return $"{currentProgress}am";
    }

    public void TestCorrectGuess() => CorrectGuess();

    public void TestIncorrectGuess() => IncorrectGuess();

    public int GetCurrentProgress()
    {
        return currentProgress;
    }
    public int GetMaxProgress()
    {
        return maxProgress;
    }
    public bool IsAtMaxProgress()
    {
        if (currentProgress >= maxProgress) {
            return true;
        }
        return false;
    }
}