using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Slider Volume;
    public TextMeshProUGUI volumeText;
    public AudioMixer audioMixer;
    public GameObject PauseMenuUI;
    void Start()
    {
        PauseMenuUI.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseMenuUI.activeSelf == true)
            {
                PauseMenuUI.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                Pause();
            }
            

        }
    }
    public void Pause()
    {
        audioMixer.GetFloat("Volume", out float volume);
        volumeText.text = "Volume " + (volume+80) + "%";
        Volume.value = volume;    
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void resume()
    {
        Time.timeScale = 1;
    }
    public void main_menu()
    {
        FindObjectOfType<AudioManager>().Play("MenuTheme");   
        FindObjectOfType<AudioManager>().Stop("GameTheme");
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1;
    }
}
