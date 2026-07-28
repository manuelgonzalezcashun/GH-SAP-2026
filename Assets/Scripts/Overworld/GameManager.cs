using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
        DontDestroyOnLoad(Instance);
    }
    #endregion

    public void EnterScene()
    {

    }
    public void LoadScene()
    {

    }
    public void UnloadScene()
    {

    }
}
