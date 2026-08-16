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

    void Update()
    {
        PauseMenuButtonSelector();

        // Pause Handling
        if (InputHandler.PauseGamePressed)
            PauseGame();
        if (InputHandler.ExitPanelPressed)
            OnResume();

        pauseMenuPanel.SetActive(gameIsPaused);
    }
    private void PauseGame()
    {
        gameIsPaused = true;
        GameManager.Instance.SetState(new GamePausedState());
    }
    public void OnResume()
    {
        GameManager.Instance.SetState(new InOverworldState());
        gameIsPaused = false;
    }
    public void OnQuest()
    {
        // Add Quest Info
    }
    public void OnMainMenu()
    {
        GameManager.Instance.SetState(new InMenuState());
        GameManager.Instance.LoadScene(mainMenuScene);
    }
    public void OnQuit()
    {
        Application.Quit();
    }

    #region Pause Menu Helper Method
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
    // Button Selection Code
    private void PauseMenuButtonSelector()
    {
        if (InputHandler.CursorToggleEnabled)
        {
            EventSystem.current.SetSelectedGameObject(null);
            buttonIndex = 0;
            return;
        }
        if (!pauseMenuPanel.activeInHierarchy) return;

        if (InputHandler.MenuSelectDown)
        {
            buttonIndex++;
            buttonIndex %= pauseMenuButtons.Length;
        }
        else if (InputHandler.MenuSelectUp)
        {
            if (buttonIndex > 0)
                buttonIndex--;
            else
                buttonIndex = pauseMenuButtons.Length - 1;
        }
        pauseMenuButtons[buttonIndex].Select();
    }
    #endregion
}
