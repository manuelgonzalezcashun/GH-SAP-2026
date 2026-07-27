using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string SceneToLoad;
    public float xDropOff;
    public float yDropOff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(SceneToLoad);

            EventBus.Raise(new SceneTransition { _X = xDropOff, _Y = yDropOff });
            Destroy(gameObject);
        }
    }
}

