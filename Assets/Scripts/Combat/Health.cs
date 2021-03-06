using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int _currentHealth;

    private bool _isInvulnerable;

    public event Action OnTakeDamage;

    public event Action OnDie;

    public bool IsDead => _currentHealth == 0;

    // Start is called before the first frame update
    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (_isInvulnerable) { return; }
        if (_currentHealth <= 0) {return;}
        _currentHealth = Mathf.Max(_currentHealth - damage, 0);
        print(_currentHealth);
        OnTakeDamage?.Invoke();
        if (_currentHealth <= 0)
        {
            OnDie?.Invoke();
        }
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        _isInvulnerable = isInvulnerable;
    }

}
