using UnityEngine;

[CreateAssetMenu(fileName = "Create new Audio Effect", menuName = "Audio/Audio Effect")]
public class AudioEffect : ScriptableObject
{
    [SerializeField] AudioClip clip = null;
    [SerializeField] bool mute = false;
    [SerializeField] bool playOnAwake = false;
    [SerializeField] bool loop = false;
    [SerializeField][Range(0, 1)] float volume = 1f;
    [SerializeField][Range(-3, 3)] float pitch = 1f;

    public AudioClip Clip => clip;
    public bool Mute => mute;
    public bool PlayOnAwake => playOnAwake;
    public bool Loop => loop;
    public float Volume => volume;
    public float Pitch => pitch;
}
