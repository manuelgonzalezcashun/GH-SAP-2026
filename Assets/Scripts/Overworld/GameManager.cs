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

    public void EnterScene(SceneReference scene)
    {
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
    }
    public void LoadScene(SceneReference scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }
    public void UnloadScene(SceneReference scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }
}
