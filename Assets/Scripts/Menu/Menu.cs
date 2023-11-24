using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void GoSlideshow()
    {
        SceneManager.LoadScene("Slideshow");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
