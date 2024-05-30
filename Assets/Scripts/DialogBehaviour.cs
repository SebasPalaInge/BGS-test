using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogBehaviour : MonoBehaviour
{
    public int queueNumber;
    public float textSpeed;
    private bool isShopkeeper = false;

    public List<string> lastTextsUsed;
    public GameObject _dialogPanel;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI generalText;

    public AudioSource audioSrc;
    public ShopInventory _shop;

    private void Start() 
    {
        _dialogPanel.SetActive(false);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(generalText.text == lastTextsUsed[queueNumber])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                generalText.text = lastTextsUsed[queueNumber];
            }
        }
    }

    public void OpenDialoguePanel(string name, List<string> textLines, bool isShopkeeper)
    {
        FindObjectOfType<PlayerBehaviour>().canMove = false;
        _dialogPanel.SetActive(true);
        queueNumber = 0;
        lastTextsUsed = textLines;
        characterName.text = name;
        generalText.text = string.Empty;
        this.isShopkeeper = isShopkeeper;   
        StartCoroutine(TypeLine());
    }

    public void CloseDialoguePanel()
    {
        if(isShopkeeper)
        {
            _shop.OpenShopInterface();
            _dialogPanel.SetActive(false); 
        }
        else
        {
            FindObjectOfType<PlayerBehaviour>().canMove = true;
            FindObjectOfType<PlayerBehaviour>().isInteracting = false;
            _dialogPanel.SetActive(false);
        }
    }
    
    private IEnumerator TypeLine()
    {
        foreach (char c in lastTextsUsed[queueNumber].ToCharArray())
        {
            generalText.text += c;
            audioSrc.pitch = Random.Range(1f,2f);
            audioSrc.Play();
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        if(queueNumber < lastTextsUsed.Count - 1)
        {
            queueNumber++;
            generalText.text = string.Empty;   
            StartCoroutine(TypeLine());
        }
        else
        {
            CloseDialoguePanel();
        }
    }
}
