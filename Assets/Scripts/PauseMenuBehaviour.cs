using TMPro;
using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour
{
    public TextMeshProUGUI title;
    public GameObject fullPanel;
    public GameObject buttonHolderPanel;
    public GameObject volumeSettingsPanel;
    public PlayerBehaviour playerBehaviour;

    private void Start() 
    {
        FullPanelState(false);
    }

    public void ExitGameButton()
    {
        Application.Quit();
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
    
}
