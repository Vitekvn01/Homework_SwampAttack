using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _weaponPoint;

    private Weapon _currentWeapon;
    private int _curentWeaponNumber = 0;
    private int _currentHealth;
    private Animator _animator;

    public Action<int, int> HealthChanged; 
    
    private void Start()
    {
        _currentWeapon = ChangeWeapon(_weapons[_curentWeaponNumber]);
        _currentHealth = _health;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot();
        }
    }
    
    private Weapon ChangeWeapon(Weapon weapon)
    {
        Weapon spawnWeapon = Instantiate(weapon.gameObject, _weaponPoint.position, quaternion.identity).GetComponent<Weapon>();
        spawnWeapon.transform.SetParent(gameObject.transform);
        return spawnWeapon;
    }
    
    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        
        HealthChanged?.Invoke(_currentHealth, _health);
        
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
