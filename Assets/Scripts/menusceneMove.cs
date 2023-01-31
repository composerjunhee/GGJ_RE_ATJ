using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menusceneMove : MonoBehaviour
{
    public void MenuGameScenesCtr()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Menu Scene Move");
    }
}
