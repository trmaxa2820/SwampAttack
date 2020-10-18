using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shootgun : Weapon
{
    [SerializeField] private int _bulletCount;
    [SerializeField] private int _minSpreadAngel;
    [SerializeField] private int _maxSpreadAngel;

    private System.Random _random = new System.Random();

    public override void Shoot(Transform shootPoint)
    {
        for (int i = 0; i < _bulletCount; i++)
        {
            Instantiate(Bullet, shootPoint.position, Quaternion.Euler(0f, 0f, _random.Next(_minSpreadAngel, _maxSpreadAngel)));
        }
    }
}
