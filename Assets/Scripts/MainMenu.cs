using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        
        SceneManager.LoadScene("Chomper");
        Time.timeScale = 1;
        enabled = true;
    }

   void Start()
   {
       
   }

   public void QuitGame()
   {
       Application.Quit();
   }
}

