using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] SceneReference sceneToLoad;
    [SerializeField] SceneReference sceneToUnload;
    // public string SceneToLoad;
    public float xDropOff;
    public float yDropOff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // DontDestroyOnLoad(gameObject);
            // SceneManager.LoadScene(SceneToLoad);

            GameManager.Instance.EnterScene(sceneToLoad);

            EventBus.Raise(new SceneTransition { _X = xDropOff, _Y = yDropOff });
            // Destroy(gameObject);
            GameManager.Instance.UnloadScene(sceneToUnload);
        }
    }
}

