using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void loadGame() {
        SceneManager.LoadScene("game");
    }

    public void loadStart() {
        SceneManager.LoadScene("StartScene");
    }
}
