using UnityEngine;

public class CelebrationState : State
{
    private const string CelebrationVariable = "Celebration";
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play(CelebrationVariable);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}
