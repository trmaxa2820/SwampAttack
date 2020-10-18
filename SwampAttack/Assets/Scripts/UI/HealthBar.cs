using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.OnHealthChanged += OnValueChanged;
        Slider.value = 1;
    }

    private void OnDisable()
    {
        _player.OnHealthChanged -= OnValueChanged;
    }
}
