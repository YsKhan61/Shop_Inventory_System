using SIS.Utilities;
using TMPro;
using UnityEngine;


namespace SIS.ShopInventory
{
    public class NotificationView : MonoBehaviour
    {
        [SerializeField]
        AnimationClip _showClip;

        [SerializeField]
        Animator _animator;

        [SerializeField]
        TextMeshProUGUI _text;

        [SerializeField]
        AudioClip _notificationSound;

        private void OnEnable()
        {
            EventService.Instance.OnItemBought.AddListener(OnItemBought);
            EventService.Instance.OnItemSold.AddListener(OnItemSold);
        }

        private void OnDisable()
        {
            EventService.Instance.OnItemBought.RemoveListener(OnItemBought);
            EventService.Instance.OnItemSold.RemoveListener(OnItemSold);
        }

        private void OnItemBought(TagSO itemTag, int quantity)
        {
            ShowNotification($"Bought {quantity} {itemTag.name}");
        }

        private void OnItemSold(TagSO itemTag, int quantity)
        {
            ShowNotification($"Sold {quantity} {itemTag.name}");
        }

        private void ShowNotification(string text)
        {
            _text.text = text;
            _animator.CrossFade(_showClip.name, 0.1f);

            EventService.Instance.OnPlayAudio.InvokeEvent(_notificationSound);
        }
    }
}
