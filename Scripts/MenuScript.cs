using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public static void StartEasyDifficulty() {
        SceneManager.LoadScene("Easy_Scene");
    }

    public static void StartMediumDifficulty() {
        SceneManager.LoadScene("Medium_Scene");
    }

    public static void StartHardDifficulty() {
        SceneManager.LoadScene("Hard_Scene");
    }
}
