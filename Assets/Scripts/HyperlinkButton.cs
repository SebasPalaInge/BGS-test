using UnityEngine;

public class HyperlinkButton : MonoBehaviour
{
    public void OpenURLLink(string webURL)
    {
        Application.OpenURL(webURL);
    }
}
