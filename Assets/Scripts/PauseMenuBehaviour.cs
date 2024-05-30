using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuBehaviour : MonoBehaviour
{
    public TextMeshProUGUI title;
    public GameObject fullPanel;
    public GameObject buttonHolderPanel;
    public GameObject volumeSettingsPanel;
    public GameObject creditsInterface;
    public PlayerBehaviour playerBehaviour;

    public AudioMixer generalMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start() 
    {
        FullPanelState(false);

        if(PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVolume();
        }

        if(PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadSFXVolume();
        }
        else
        {
            SetSFXVolume();
        }
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

    public void OpenCreditsInterface()
    {
        creditsInterface.SetActive(true);
        fullPanel.SetActive(false);
    }

    public void CloseCreditsInterface()
    {
        creditsInterface.SetActive(false);
        fullPanel.SetActive(true);
    }

    public void FullPanelState(bool state)
    {
        fullPanel.SetActive(state);
        playerBehaviour.canMove = !state;
        if(state)
            title.text = "Paused";
    }

    public void FullPanelState(bool state, bool changePlayerMove)
    {
        fullPanel.SetActive(state);
        if(state)
            title.text = "Paused";
        if(changePlayerMove)
            playerBehaviour.canMove = !state;
    }

    public void ButtonHolderState(bool state)
    {
        buttonHolderPanel.SetActive(state);
    }

    public void VolumeSettingsState(bool state)
    {
        ButtonHolderState(!state);
        volumeSettingsPanel.SetActive(state);
        if(state)
            title.text = "Volume";
        else
            title.text = "Paused";
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        generalMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        generalMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
    }

    public void LoadSFXVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSFXVolume();
    }
}
