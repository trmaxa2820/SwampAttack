using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    private int _currentHealth;
    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private Animator _animator;

    public event UnityAction<int, int> OnHealthChanged;
    public event UnityAction<int> MoneyChanged;
    public int Money { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _currentWeapon = _weapons[_currentWeaponNumber];
        _currentHealth = _health;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot(_shootPoint);
        }      
    }

    public void OnEnemyDied(int reward)
    {
        Money += reward;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        OnHealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
            Destroy(gameObject);
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        _weapons.Add(weapon);
        MoneyChanged?.Invoke(Money);
    }

    public void NextWeapon()
    {
        _currentWeaponNumber = _currentWeaponNumber == (_weapons.Count - 1) ? 0 : ++_currentWeaponNumber;
        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        _currentWeaponNumber = _currentWeaponNumber == 0 ? _weapons.Count - 1 : --_currentWeaponNumber;
        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
}
