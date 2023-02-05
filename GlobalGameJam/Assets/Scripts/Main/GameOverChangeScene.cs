using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverChangeScene : MonoBehaviour
{
    public void LoadNextScene()
    {
        Debug.Log("restart");
        SceneManager.LoadScene(1);
    }
}
