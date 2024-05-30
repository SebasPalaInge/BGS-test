using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "InteractableTexts", menuName = "Interactables")]
public class InteractableObject : ScriptableObject
{
    public string dialogName = "Player";
    [TextArea] public List<string> textsLines;
}
