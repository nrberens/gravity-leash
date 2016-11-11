using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;
    static bool AudioBegin = false;
    void Awake()
    {
        if (!AudioBegin)
        {
            GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(gameObject);
            AudioBegin = true;
        }
    }
    void Update()
    {
    }
}
