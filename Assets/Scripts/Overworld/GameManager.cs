using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : StateMachine
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
    [SerializeField] AudioEffect overworldTheme = null;
    public GameState CurrentState => _currentState as GameState;
    public AudioEffect OverworldTheme => overworldTheme;
    void Start()
    {
        SetState(new InOverworldState());
    }
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
