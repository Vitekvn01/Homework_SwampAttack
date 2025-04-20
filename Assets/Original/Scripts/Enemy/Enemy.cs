using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;

    private Player _target;

    public Action<Enemy> OnDieEvent;
    
    public int Money { get; private set; }

    public void Init(Player player)
    {
        _target = player;
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            OnDieEvent?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public Player GetTarget()
    {
        return _target;
    }
}
