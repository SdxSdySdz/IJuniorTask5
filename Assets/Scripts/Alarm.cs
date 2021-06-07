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
    private Coroutine _currentCoroutine;

    private float _volume
    {
        get
        {
            return _audioSource.volume;
        }
        
        set
        {
            _audioSource.volume = value;
        }
    }
    private bool _isMaxVolumeReached => _volume == 1;
    private bool _isMinVolumeReached => _volume == 0;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _minVolume = 0;
        _maxVolume = 1;

        _volume = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            
            _currentCoroutine = StartCoroutine(Activate());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(Deactivate());
        }
    }

    private IEnumerator Activate()
    {
        while (_isMaxVolumeReached == false)
        {
            _volume = Mathf.MoveTowards(_volume, _maxVolume, _volumeChangingRate * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator Deactivate()
    {
        while (_isMinVolumeReached == false)
        {
            _volume = Mathf.MoveTowards(_volume, _minVolume, _volumeChangingRate * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
