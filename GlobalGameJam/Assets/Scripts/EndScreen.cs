using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndScreen : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject buttons;

    private void Awake()
    {
        Invoke(nameof(StartCouRout), 0.3f);
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
