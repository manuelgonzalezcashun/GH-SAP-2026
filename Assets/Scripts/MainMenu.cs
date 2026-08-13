using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    List<AsyncOperation> loadingOperations = new List<AsyncOperation>();

    [Header("Mute Controls")]
    [SerializeField] Button musicOnButton = null;
    [SerializeField] Button musicOffButton = null;
    [Header("Panels")]
    [SerializeField] GameObject mainMenuPanel = null;
    [SerializeField] GameObject controlsPanel = null;
    [SerializeField] GameObject cutScene = null;
    [SerializeField] GameObject loadingScreen = null;
    [SerializeField] GameObject fogParticles = null;
    [Header("Main Menu Theme")]
    [SerializeField] AudioEffect mainMenuAudioEffect;
    [Header("Scenes")]
    [SerializeField] SceneReference mainMenuScene = null;
    [SerializeField] SceneReference coreScene = null;
    [SerializeField] SceneReference levelOneScene = null;

    [Header("Main Menu Buttons")]
    [SerializeField] Button[] mainMenuButtons = null;
    [SerializeField] Button nextLevelButton = null;
    [SerializeField] Button controlsExitButton = null;
    int buttonIndex = 0;

    void OnEnable()
    {
        foreach (var button in mainMenuButtons)
        {
            button.onClick.AddListener(() => EventSystem.current.SetSelectedGameObject(null));
            button.onClick.AddListener(() => buttonIndex = 0);

        }
    }
    void OnDisable()
    {
        foreach (var button in mainMenuButtons)
        {
            button.onClick.RemoveAllListeners();
            Debug.Log("Removing All Listeners");
        }
    }

    void Start()
    {
        InputHandler.ChangeActionMaps(InputHandler.mainMenuInput);
        EventBus.Raise(new PlayAudioEvent { audioEffect = mainMenuAudioEffect });
    }
    void Update()
    {
        MainMenuButtonSelector();
        if (controlsPanel.activeInHierarchy && InputHandler.ExitPanelPressed)
        {
            StopAllCoroutines();
            OnControlsExit();
        }
    }

    private void MainMenuButtonSelector()
    {
        if (!mainMenuPanel.activeInHierarchy)
        {
            nextLevelButton.Select();
            return;
        }

        if (InputHandler.MainMenuSelectDown)
        {
            buttonIndex++;
            buttonIndex %= mainMenuButtons.Length;
        }
        else if (InputHandler.MainMenuSelectUp)
        {
            if (buttonIndex > 0)
                buttonIndex--;
            else
                buttonIndex = mainMenuButtons.Length - 1;
        }
        mainMenuButtons[buttonIndex].Select();
    }
    public void EnterCoreScene()
    {
        // Load Scenes
        StartCoroutine(RunLoadingOperations());

        // Stop Main Menu Music
        EventBus.Raise(new StopAudioEvent { audioEffect = mainMenuAudioEffect });
    }
    IEnumerator RunLoadingOperations()
    {
        nextLevelButton.enabled = false;
        loadingScreen.SetActive(true);

        var coreOperation = SceneManager.LoadSceneAsync(coreScene, LoadSceneMode.Additive);
        var levelOperation = SceneManager.LoadSceneAsync(levelOneScene, LoadSceneMode.Additive);

        loadingOperations.Add(coreOperation);
        loadingOperations.Add(levelOperation);

        for (int i = 0; i < loadingOperations.Count; i++)
        {
            while (!loadingOperations[i].isDone)
            {
                yield return null;
            }
        }
        var activeCoreScene = SceneManager.GetSceneByName(coreScene.SceneName);
        SceneManager.SetActiveScene(activeCoreScene);

        AsyncOperation unloadMenu = SceneManager.UnloadSceneAsync(mainMenuScene);
        while (!unloadMenu.isDone) yield return null;

        loadingScreen.SetActive(false);
    }
    public void OnPlay()
    {
        mainMenuPanel.SetActive(false);
        musicOnButton.gameObject.SetActive(false);
        musicOffButton.gameObject.SetActive(false);
        fogParticles.SetActive(false);

        cutScene.SetActive(true);
    }
    public void OnControls()
    {
        StartCoroutine(ControlsSequence());
    }
    IEnumerator ControlsSequence()
    {
        // Display Controls Panel
        controlsPanel.SetActive(true);
        fogParticles.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        // Hide Main Menu Panel
        mainMenuPanel.SetActive(false);
    }
    public void OnControlsExit()
    {
        // Hide Controls Panel
        controlsPanel.SetActive(false);

        // Display Main Menu
        mainMenuPanel.SetActive(true);
        fogParticles.SetActive(true);
    }
    public void OnQuit()
    {
        // Quit Game
    }
    public void OnMute(bool toggle)
    {
        // Toggle Mute Icon
        musicOnButton.gameObject.SetActive(!toggle);
        musicOffButton.gameObject.SetActive(toggle);

        // Toggle whether music is muted
        AudioManager.Instance.Mute(toggle);
    }
}
