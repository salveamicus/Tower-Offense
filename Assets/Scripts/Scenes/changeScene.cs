using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene : MonoBehaviour
{
    public void loadGame() {
        SceneManager.LoadScene("game");
    }

    public void loadStart() {
        SceneManager.LoadScene("StartScene");
    }
}
