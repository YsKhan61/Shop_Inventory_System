using UnityEngine;
using UnityEngine.EventSystems;

namespace SIS.Utilities
{
    public class OnClickBehaviour : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] 
        AudioClip _clickSound;


        public void OnPointerDown(PointerEventData eventData)
        {
            EventService.Instance.OnPlayAudio.InvokeEvent(_clickSound);
        }
    }
}

