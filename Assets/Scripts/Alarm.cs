using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _volumeChangingRate;
    private AudioSource _audioSource;
    private float _minVolume;
    private float _maxVolume;
    private Coroutine _volumeChangingJob;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _minVolume = 0;
        _maxVolume = 1;

        _audioSource.volume = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover player))
        {
            if (_volumeChangingJob != null)
            {
                StopCoroutine(_volumeChangingJob);
            }

            Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover player))
        {
            if (_volumeChangingJob != null)
            {
                StopCoroutine(_volumeChangingJob);
            }

            Deactivate();
        }
    }

    private IEnumerator ChangeVolume(float _targetVolume)
    {
        while (_audioSource.volume != _targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _volumeChangingRate * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
        }

        yield break;
    }

    private void Activate()
    {
        _volumeChangingJob = StartCoroutine(ChangeVolume(_maxVolume));
    }

    private void Deactivate()
    {
        _volumeChangingJob = StartCoroutine(ChangeVolume(_minVolume));
    }
}
