using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string SceneToLoad;
    public int xDropOff;
    public int yDropOff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneToLoad);

            EventBus.Raise<SceneTransition>(new SceneTransition{_X = xDropOff, _Y = yDropOff});
        }
    }
}
