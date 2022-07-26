using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int _currentHealth;

    public event Action OnTakeDamage;

    // Start is called before the first frame update
    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (_currentHealth <= 0) { return; }
        _currentHealth = Mathf.Max(_currentHealth - damage, 0);
        print(_currentHealth);
        OnTakeDamage?.Invoke();
    }

}
