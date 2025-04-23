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
    public int Money { get; private set; }
    
    public event Action<int, int> HealthChanged; 
    public event Action<int> MoneyChanged;
    
    private void Start()
    {
        ChangeWeapon(_weapons[_curentWeaponNumber]);
        _currentHealth = _health;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            _currentWeapon.Shoot();
        }
    }
    
    private void ChangeWeapon(Weapon weapon)
    {
        if (_currentWeapon != null)
        {
            Destroy(_currentWeapon.gameObject);
        }
        
        Weapon spawnWeapon = Instantiate(weapon.gameObject, _weaponPoint.position, quaternion.identity).GetComponent<Weapon>();
        spawnWeapon.transform.SetParent(gameObject.transform);
        
        _currentWeapon = spawnWeapon;
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
    
    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        _weapons.Add(weapon);
    }
    
    public void NextWeapon()
    {
        if (_curentWeaponNumber == _weapons.Count - 1)
        {
            _curentWeaponNumber = 0;
        }
        else
        {
            _curentWeaponNumber++;
        }

        ChangeWeapon(_weapons[_curentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_curentWeaponNumber == 0)
        {
            _curentWeaponNumber = _weapons.Count - 1;
        }
        else
        {
            _curentWeaponNumber--;
        }

        ChangeWeapon(_weapons[_curentWeaponNumber]);
    }
}
