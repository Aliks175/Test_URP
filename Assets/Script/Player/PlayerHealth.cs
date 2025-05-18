using System;
using UnityEngine;

public interface Health
{
    public void TakeDamage();
}

public class PlayerHealth : MonoBehaviour, Health
{
    [SerializeField] private int _startHealth = 3;

    private int health;
    public Action OnHited;
    public Action OnDied;

    public void TakeDamage()
    {
        health -= 1;
        Debug.Log(health);
        if (health <= 0)
        {
        Debug.Log(" Я мертв ");
            Die();
        }
        OnHited?.Invoke();
    }

    public void Respawn()
    {
        health = _startHealth;
    }

    private void Die()
    {
        OnDied?.Invoke();
    }
}
