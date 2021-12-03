using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
   //public GameObject yourChildObj;
    public Camera Camera;
    public GameObject P;
    public GameObject Pa;
    public GameObject PaddleDecider;
    public GameObject AIPaddle;
    public GameObject PlayerPaddle;
    //public AsyncOperation asyncOperation;
    
    void Start()
    {   
        //asyncOperation = SceneManager.LoadSceneAsync("PongPlay");
        
        DontDestroyOnLoad(PaddleDecider);
        
        //SceneManager.LoadScene("PongPlay", LoadSceneMode.Additive);
        //asyncOperation.allowSceneActivation = false;
        //yourChildObj.transform.parent = null;
       // DontDestroyOnLoad(yourChildObj);
    }
        
    public void PlayGame1Player()
    {
        //Time.timeScale = 1;
        SceneManager.sceneLoaded -= PlayGameSingle;
        SceneManager.sceneLoaded -= PlayGameDouble; 
        SceneManager.sceneLoaded += PlayGameSingle;
        SceneManager.LoadScene("PongPlay");
        //SceneManager.sceneLoaded += FindObjectOfType<AudioManager>().PlayGameTheme;
        
    }
    public void PlayGameSingle(Scene scene, LoadSceneMode mode)
    {
        
        //SceneManager.LoadScene("PongPlay");
        Scene scene2 = SceneManager.GetActiveScene();
        GameObject go = GameObject.Find("GoodCanvas");
        if(scene2.name == "PongPlay")
        {
            PaddleDecider.SetActive(false);
            AIPaddle = GameObject.Find("AIpaddle");
            PlayerPaddle = GameObject.Find("PaddleRight");
            AIPaddle.SetActive(true);
            PlayerPaddle.SetActive(false);
            FindObjectOfType<AudioManager>().PlayGameTheme();
            //SceneManager.sceneLoaded -= PlayGameSingle;
        }
        if(scene2.name == "PongPlay")
        {
            //SceneManager.sceneLoaded -= PlayGameSingle;
        }
        if(scene2.name == "StartMenu")
        {
            Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Destroy(go);
            PaddleDecider.SetActive(true);
            P.SetActive(true);
            Pa.SetActive(true);
            GameObject.Find("PlayerMenu").SetActive(false);
            Canvas Canvas = PaddleDecider.GetComponent<Canvas>();
            Canvas.worldCamera = Camera;
        }
    }
    public void PlayGame2Player()
    {
        //Time.timeScale = 1;
        SceneManager.sceneLoaded -= PlayGameDouble;
        SceneManager.sceneLoaded -= PlayGameSingle;
        SceneManager.sceneLoaded += PlayGameDouble;
        SceneManager.LoadScene("PongPlay");
    }
    public void PlayGameDouble(Scene scene, LoadSceneMode mode)
    {
        
        //SceneManager.LoadScene("PongPlay");
        //scene = SceneManager.GetSceneByName("PongPlay");
        Scene scene2 = SceneManager.GetActiveScene();
        GameObject go = GameObject.Find("GoodCanvas");
        if(scene2.name == "PongPlay")
        {
            PaddleDecider.SetActive(false);
            AIPaddle = GameObject.Find("AIpaddle");
            PlayerPaddle = GameObject.Find("PaddleRight");
            AIPaddle.SetActive(false);
            PlayerPaddle.SetActive(true);
            FindObjectOfType<AudioManager>().PlayGameTheme();
            //SceneManager.sceneLoaded -= PlayGameSingle;
        }
        if(scene2.name == "PongPlay")
        {
            //SceneManager.sceneLoaded -= PlayGameDouble;
        }
        if(scene2.name == "StartMenu")
        {
            Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Destroy(go);
            PaddleDecider.SetActive(true);
            P.SetActive(true);
            Pa.SetActive(true);
            GameObject.Find("PlayerMenu").SetActive(false);
            Canvas Canvas = PaddleDecider.GetComponent<Canvas>();
            Canvas.worldCamera = Camera;
        }
    }
    
}
