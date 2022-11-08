using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSignaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;

    private bool _isAlarm = false;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private float _speed = 0.001f;

    private void Start()
    {
        _audio.Play();
        _audio.Pause();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            if (_isAlarm == false)
            {
               _audio.UnPause();
                ActivatedAlarm();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(_isAlarm)
        {
            _audio.UnPause();
            DeactivatedAlarm();
        }
    }

    private void DeactivatedAlarm()
    {
        _isAlarm = false;
        StartCoroutine(UseAlarm());
    }

    private void ActivatedAlarm()
    {
        _isAlarm = true;
        StartCoroutine(UseAlarm());
    }

    private IEnumerator UseAlarm()
    {
        while(_isAlarm == false)
        {
            if (_audio.volume >= _minVolume)
            {
                _audio.volume -= Mathf.MoveTowards(_minVolume, _maxVolume, _speed);
                yield return null;
            }
            else
            {
                _audio.Pause();
                _isAlarm = true;
            }
        }

        while (_isAlarm)
        {
            if (_audio.volume <= _maxVolume)
            {
                _audio.volume += Mathf.MoveTowards(_minVolume, _maxVolume, _speed);
                yield return null;
            }
            else
            {  
                _isAlarm = false;
            }
        }
    }

    private IEnumerator LowerAlarm()
    {
        bool isWork = true;

        while (isWork)
        {
            if (_audio.volume > _minVolume)
            {
                _audio.volume -= Mathf.MoveTowards(_minVolume, _maxVolume, _speed);
                yield return null;
            }
            else
            {
                isWork = false;
                _audio.Pause();
            }
        }
    }
}