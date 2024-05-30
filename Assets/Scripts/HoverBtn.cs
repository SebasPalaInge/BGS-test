using UnityEngine;
using UnityEngine.EventSystems;

public class HoverBtn : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSfx;
    public AudioClip clickSfx;

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponentInParent<AudioSource>().PlayOneShot(clickSfx);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<AudioSource>().PlayOneShot(hoverSfx);
    }
}
