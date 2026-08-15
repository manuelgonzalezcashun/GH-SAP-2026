using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button[] pauseMenuButtons = null;
    [SerializeField] GameObject pauseMenuPanel = null;
    [SerializeField] SceneReference mainMenuScene = null;
    private bool gameIsPaused = false;
    private int buttonIndex = 0;
    private int delay = 200; // in milliseconds
                             // void OnEnable()
                             // {
                             //     foreach (var button in pauseMenuButtons)
                             //     {
                             //         button.onClick.AddListener(() => ResetButtonState());
                             //     }
                             // }
                             // void OnDisable()
                             // {
                             //     foreach (var button in pauseMenuButtons)
                             //     {
                             //         button.onClick.RemoveAllListeners();
                             //     }
                             // }

    void Update()
    {
        if (InputHandler.PauseGamePressed)
            PauseGame();

        pauseMenuPanel.SetActive(gameIsPaused);
    }
    private void PauseGame()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            GameManager.Instance.SetState(new GamePausedState());
        }
        else OnResume();
    }
    public void OnResume()
    {
        GameManager.Instance.SetState(new InOverworldState());
        gameIsPaused = false;

        Time.timeScale = 1f;
    }
    public void OnQuest()
    {
        // Add Quest Info
    }
    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.LoadScene(mainMenuScene);
    }
    public void OnQuit()
    {
        Application.Quit();
    }
    private async void ResetButtonState()
    {
        EventSystem.current.SetSelectedGameObject(null);
        var currentButton = pauseMenuButtons[buttonIndex];
        var normalState = currentButton.colors.normalColor;

        if (!currentButton.gameObject.activeInHierarchy) return; // Not a bug, but helps with performance

        currentButton.transition = Selectable.Transition.None;
        currentButton.image.color = normalState;

        await Task.Delay(delay);

        currentButton.transition = Selectable.Transition.ColorTint;
        buttonIndex = 0;
    }
}
