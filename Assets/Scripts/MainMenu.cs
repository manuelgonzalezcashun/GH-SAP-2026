using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Controls")]
    [SerializeField] Button playButton = null;
    [SerializeField] Button controlsButton = null;
    [SerializeField] Button quitButton = null;
    [Header("Mute Controls")]
    [SerializeField] Button musicOnButton = null;
    [SerializeField] Button musicOffButton = null;
    [Header("Panel")]
    [SerializeField] GameObject mainMenuPanel = null;
    [Header("Main Menu Theme")]
    [SerializeField] AudioEffect mainMenuAudioEffect;
    [Header("Scenes")]
    [SerializeField] SceneReference systemsScene = null;
    [SerializeField] SceneReference levelOneScene = null;

    void Start()
    {
        EventBus.Raise(new PlayAudioEvent { audioEffect = mainMenuAudioEffect });
    }

    public void OnPlay()
    {
        // Enter Systems Scene Async
        SceneManager.LoadSceneAsync(systemsScene);
        // Enter Level One Scene Asyc, Additive
        SceneManager.LoadSceneAsync(levelOneScene, LoadSceneMode.Additive);
        // Stop Main Menu Music
        EventBus.Raise(new StopAudioEvent { audioEffect = mainMenuAudioEffect });
    }
    public void OnControls()
    {
        // Hide Main Menu Panel
        mainMenuPanel.SetActive(false);

        // Display Controls Panel
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
