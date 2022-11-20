using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] PlayerMove _playerMove;
    [SerializeField] PreFinishBehaviour _preFinishBehevior;
    [SerializeField] Animator _animator;

    void Start()
    {
        _playerMove.enabled = false;
        _preFinishBehevior.enabled = false;
    }

    public void Play()
    {
        _playerMove.enabled = true;
    }

    public void StartPreFinishBehaviour()
    {
        _playerMove.enabled = false;
        _preFinishBehevior.enabled = true;
    }

    public void StartFinishBehaviour()
    {
        _preFinishBehevior.enabled = false;
        _animator.SetTrigger("Dance");
    }

}

