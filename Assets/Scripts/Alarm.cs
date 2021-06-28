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
    private Coroutine _volumeChangingCoroutine;

    public float Volume
    {
        get
        {
            return _audioSource.volume;
        }

        private set
        {
            _audioSource.volume = value;
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _minVolume = 0;
        _maxVolume = 1;

        Volume = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_volumeChangingCoroutine != null)
            {
                StopCoroutine(_volumeChangingCoroutine);
            }

            Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_volumeChangingCoroutine != null)
            {
                StopCoroutine(_volumeChangingCoroutine);
            }

            Deactivate();
        }
    }

    private IEnumerator ChangeVolume(float _targetVolume)
    {
        while (Volume != _targetVolume)
        {
            Volume = Mathf.MoveTowards(Volume, _targetVolume, _volumeChangingRate * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void Activate()
    {
        _volumeChangingCoroutine = StartCoroutine(ChangeVolume(_maxVolume));
    }

    private void Deactivate()
    {
        _volumeChangingCoroutine = StartCoroutine(ChangeVolume(_minVolume));
    }
}
