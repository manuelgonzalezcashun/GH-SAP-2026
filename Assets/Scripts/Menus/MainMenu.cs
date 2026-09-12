using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    [Header("Runtime Variables")]
    [SerializeField] int delay = 200; // delay in ms
    [SerializeField] Camera mainCamera = null;
    [SerializeField] Animator cutsceneAnimation = null;
    int buttonIndex = 0;

    void OnEnable()
    {
        foreach (var button in mainMenuButtons) button.onClick.AddListener(() => ResetButtonState());
        musicOnButton.onClick.AddListener(() => ResetButtonState());
        musicOffButton.onClick.AddListener(() => ResetButtonState());

        mainMenuButtons[0].Select();
    }
    void OnDisable()
    {
        foreach (var button in mainMenuButtons) button.onClick.RemoveAllListeners();
        musicOnButton.onClick.RemoveAllListeners();
        musicOffButton.onClick.RemoveAllListeners();
    }
    void Start()
    {
        InputHandler.ChangeActionMaps(InputHandler.menuInput);
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
        if (InputHandler.CutsceneSkipPressed)
        {
            SkipCutscene();
        }
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
    private IEnumerator ControlsSequence()
    {
        controlsPanel.SetActive(true);
        fogParticles.SetActive(false);

        float s_Delay = delay / 1000f; // convert delay from ms into secs
        yield return new WaitForSeconds(s_Delay);
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
        Application.Quit();
    }
    public void OnMute(bool toggle)
    {
        // Toggle Mute Icon
        musicOnButton.gameObject.SetActive(!toggle);
        musicOffButton.gameObject.SetActive(toggle);
        buttonIndex = 0;

        // Toggle whether music is muted
        AudioManager.Instance.Mute(toggle);
    }
    private void SkipCutscene()
    {
        string skipCutsceneHash = "CutsceneEnd";
        if (!cutScene.activeInHierarchy) return;

        AnimatorStateInfo sceneInfo = cutsceneAnimation.GetCurrentAnimatorStateInfo(0);
        if (sceneInfo.IsName(skipCutsceneHash) || sceneInfo.normalizedTime < 0.05f) return;

        cutsceneAnimation.CrossFade(skipCutsceneHash, 0f);
        nextLevelButton.Select();
    }

    #region Main Menu Helper Methods
    private void MainMenuButtonSelector()
    {
        if (InputHandler.CursorToggleEnabled)
        {
            EventSystem.current.SetSelectedGameObject(null);
            buttonIndex = 0;
            return;
        }
        if (!mainMenuPanel.activeInHierarchy)
        {
            nextLevelButton.Select();
            return;
        }

        if (InputHandler.MenuSelectDown)
        {
            buttonIndex++;
            buttonIndex %= mainMenuButtons.Length;
        }
        else if (InputHandler.MenuSelectUp)
        {
            if (buttonIndex > -1)
                buttonIndex--;
            else
                buttonIndex = mainMenuButtons.Length - 1;
        }

        if (buttonIndex <= -1 || buttonIndex >= mainMenuButtons.Length) return;
        mainMenuButtons[buttonIndex].Select();
    }
    public void EnterCoreScene()
    {
        mainCamera.gameObject.SetActive(false);

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
        if (GameManager.Instance != null) GameManager.Instance.SetState(new InOverworldState());

        AsyncOperation unloadMenu = SceneManager.UnloadSceneAsync(mainMenuScene);
        while (!unloadMenu.isDone) yield return null;

        loadingScreen.SetActive(false);
    }
    // After button is pressed, return to normal state
    private async void ResetButtonState()
    {
        EventSystem.current.SetSelectedGameObject(null);
        var currentButton = mainMenuButtons[buttonIndex];
        var normalState = currentButton.animationTriggers.normalTrigger;

        if (!currentButton.gameObject.activeInHierarchy) return; // Not a bug, but helps with performance

        currentButton.transition = Selectable.Transition.None;
        currentButton.animator.CrossFade(normalState, 0);

        await Task.Delay(delay);

        currentButton.transition = Selectable.Transition.Animation;
        buttonIndex = 0;
    }
    #endregion
}