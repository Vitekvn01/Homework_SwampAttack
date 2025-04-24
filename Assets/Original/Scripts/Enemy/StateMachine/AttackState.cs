using UnityEngine;

public class AttackState : State
{
    private const string AttackVariable = "Attack";
    
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;

    private float _lastAttackTime;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_lastAttackTime <= 0 )
        {
            Attack(Target);
            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack(Player target)
    {
        _animator.Play(AttackVariable);
        target.ApplyDamage(_damage);
    }
}
