using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    public int qualityIndex;
    public TMP_Dropdown graphuics;
    public int graphicval;
    public Slider Volume;
    public TextMeshProUGUI VolumePercent;
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        VolumePercent.text = "Volume " + (volume+80) + "%";
    }
    public void set_volumeStart()
    {
        audioMixer.GetFloat("Volume", out float volume);   
        VolumePercent.text = VolumePercent.text = "Volume " + (volume+80) + "%";
        Volume.value = volume; 
    }
    public void setQuality()
    {
        
        qualityIndex = graphuics.value;
        //qualityIndex = graphuics.value;
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void setFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
    public void setGraphics()
    {
        graphuics.value = qualityIndex;
    }
}
