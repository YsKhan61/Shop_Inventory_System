using UnityEngine;

namespace SIS.Utilities
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioService : MonoBehaviour
    {
        AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            EventService.Instance.OnPlayAudio.AddListener(OnPlayAudio);
        }

        private void OnDisable()
        {
            EventService.Instance.OnPlayAudio.RemoveListener(OnPlayAudio);
        }

        private void OnPlayAudio(AudioClip audioClip)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(audioClip);
        }
    }
}

