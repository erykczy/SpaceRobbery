using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour
{
    public float MaxHealth;
    private float _health;
    public float Health
    {
        get => _health;
        private set
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);
            if (_health == 0) Destroy();
        }
    }

    private void Awake()
    {
        Health = MaxHealth;
    }

    public void Damage(float damage)
    {
        Health -= damage;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
