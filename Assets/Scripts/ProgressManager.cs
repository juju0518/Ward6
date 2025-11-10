using UnityEngine;
using UnityEngine.Events;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private int maxProgress = 5;
    [SerializeField] private Transform hourHand;
    
    public UnityEvent<int> OnProgressChanged;
    public UnityEvent OnProgressReset;
    public UnityEvent OnMaxProgressReached;
    private int currentProgress = 0;

    private Vector3[] hourRotations = new Vector3[]
    {
        new Vector3(-0.28f, 0f, 0f),//midnight
        new Vector3(-0.794f, 1.9f, 29.917f), //1
        new Vector3(-1.84f, 3.07f, 53.691f), //2
        new Vector3(-4.07f, 3.811f, 89.713f), //3
        new Vector3(-5.117f, 3.22f, 122.11f),//4
        new Vector3(-6.22f, 2.158f, 145.48f),//5
        new Vector3(-7.88f, 0.014f, 179.78f)//6
        //new Vector3(-7.295f, -2.049f, 212.54f)//7
        //new Vector3(-6.43f, -3.006f, 232.1f)//8
    };

    void Start()
    {
        if (hourHand == null)
        {
            GameObject clock = GameObject.Find("Clock");
            if (clock != null)
            {
                Transform arrow = clock.transform.Find("Arrow");
                if (arrow != null)
                {
                    hourHand = arrow.Find("HourHand");
                }
            }
        }
        
        InitializeClock();
    }

    public void CorrectGuess()
    {
        currentProgress++;
        Debug.Log($"correct! progress: {currentProgress}");
        
        OnProgressChanged?.Invoke(currentProgress);
        UpdateClock();
        
        if (currentProgress >= maxProgress)
        {
            Debug.Log("max progress reached");
            OnMaxProgressReached?.Invoke();
        }
    }

    public void IncorrectGuess()
    {
        Debug.Log($"incorrect! progress reset from {currentProgress} to 0");
        currentProgress = 0;
        
        OnProgressReset?.Invoke();
        OnProgressChanged?.Invoke(currentProgress);
        UpdateClock();
    }

    private void InitializeClock()
    {
        UpdateClock();
    }

    private void UpdateClock()
    {
        if (hourHand == null)
        {
            Debug.LogWarning("hour hand not found");
            return;
        }

        if (currentProgress >= 0 && currentProgress < hourRotations.Length)
        {
            Vector3 targetRotation = hourRotations[currentProgress];
            hourHand.localRotation = Quaternion.Euler(targetRotation);
            
            Debug.Log($"clock updated to: {GetTimeString()} (rotation: {targetRotation})");
        }
        else
        {
            Debug.LogWarning($"progress {currentProgress} is out of range");
        }
    }

    private string GetTimeString()
    {
        if (currentProgress == 0) return "12:00 (Midnight)";
        return $"{currentProgress}:00";
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
        return currentProgress >= maxProgress;
    }

    [System.Serializable]
    public class HourRotation
    {
        public int hour;
        public Vector3 rotation;
    }

    [SerializeField] private HourRotation[] inspectorHourRotations;

    void OnValidate()
    {
        if (inspectorHourRotations != null && inspectorHourRotations.Length > 0)
        {
            for (int i = 0; i < inspectorHourRotations.Length && i < hourRotations.Length; i++)
            {
                if (inspectorHourRotations[i].hour >= 0 && inspectorHourRotations[i].hour < hourRotations.Length)
                {
                    hourRotations[inspectorHourRotations[i].hour] = inspectorHourRotations[i].rotation;
                }
            }
        }
    }
}