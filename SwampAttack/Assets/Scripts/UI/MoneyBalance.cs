using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _money.text = _player.Money.ToString();
        _player.MoneyChanged += OnMoneyChange;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= OnMoneyChange;
    }

    private void OnMoneyChange(int money)
    {
        _money.text = money.ToString();
    }
}
