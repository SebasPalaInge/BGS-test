using UnityEngine;

public class PropInteract : MonoBehaviour
{
    public bool isShopkeeper = false;
    public bool canInteract = false;

    public InteractableObject _object;
    public GameObject interactionIcon;
    private PlayerBehaviour _player;

    private void Start() 
    {
        _player = FindObjectOfType<PlayerBehaviour>();
    }

    private void Update() 
    {
        if(canInteract)
        {
            OpenInteractPrompt();
        }
        else
        {
            interactionIcon.SetActive(false);
        }    
    }
    
    private void OpenInteractPrompt()
    {
        if(_player.isInteracting) return;

        interactionIcon.SetActive(true);
        if(Input.GetKeyDown(KeyCode.E))
        {
            _player.isInteracting = true;
            interactionIcon.SetActive(false);
            FindObjectOfType<DialogBehaviour>().OpenDialoguePanel(_object.dialogName, _object.textsLines, isShopkeeper);
        }
    } 
}
