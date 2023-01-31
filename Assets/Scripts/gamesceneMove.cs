using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamesceneMove : MonoBehaviour
{
    public void GameScenesCtr()
    {
        SceneManager.LoadScene("GameScene");
        Debug.Log("Game Scenes Move");
    }
}
