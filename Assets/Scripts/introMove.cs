using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introMove : MonoBehaviour
{
    public void GameScenesCtr()
    {
        SceneManager.LoadScene("Intro");
        Debug.Log("Intro Scene Move");
    }
}
