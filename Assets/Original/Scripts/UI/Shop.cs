using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _template;
    [SerializeField] private GameObject _itemConteiner;

    private List<WeaponView> _weaponViews = new List<WeaponView>();

    private void Start()
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            AddItem(_weapons[i]);
        }
    }

    private void OnDestroy()
    {
        foreach (var view in _weaponViews)
        {
            if (view != null)
            {
                view.SellButtonClick -= OnSellButtonClick;
            }
        }
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_template, _itemConteiner.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(weapon);
        _weaponViews.Add(view);
    }

    private void OnSellButtonClick(Weapon weapon, WeaponView view)
    {
        TrySellWeapon(weapon, view);
    }

    private void TrySellWeapon(Weapon weapon, WeaponView view)
    {
        if (weapon.Price <= _player.Money)
        {
            _player.BuyWeapon(weapon);
            weapon.Buy();
            view.SellButtonClick -= OnSellButtonClick;
            _weaponViews.Remove(view);
        }
    }
}
