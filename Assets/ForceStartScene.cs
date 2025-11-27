using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceStartScene : MonoBehaviour
{
    private string StartSceneName = "StartingScene";

    private static bool hasStarted = false;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name != StartSceneName && !hasStarted)
        {
            hasStarted = true;
            SceneManager.LoadScene(StartSceneName);
        }
    }
}
