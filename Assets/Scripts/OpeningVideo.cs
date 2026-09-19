using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class OpeningVideo : MonoBehaviour
{
    [SerializeField] Button continueButton = null;
    private VideoPlayer videoPlayer => GetComponent<VideoPlayer>();
    void Start()
    {
        if (videoPlayer == null) return;

        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "Opening Animation Sequence.mp4");
        videoPlayer.Play();

        if (continueButton == null) return;
        continueButton.Select();
    }
}
