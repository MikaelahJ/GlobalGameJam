using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject buttons;

    public AudioClip overNarrator;

    private void Awake()
    {
        Invoke(nameof(StartCouRout), 1f);
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            AudioManager.Instance.PlayNarrator(overNarrator);
        }
    }
    private void StartCouRout()
    {
        StartCoroutine(IsVideoPlaying());
    }

    private IEnumerator IsVideoPlaying()
    {
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        buttons.SetActive(true);
    }
}
