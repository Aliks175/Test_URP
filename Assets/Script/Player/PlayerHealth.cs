using System;
using UnityEngine;

public interface Health
{
    public void TakeDamage();
    public void TakeHealth(int health);
    public void ChangeMaxHealth(int health);
}

public class PlayerHealth : Health
{
    public int Health { get { return _health; } private set { _health = value; } }
    public int MaxHealth { get { return _maxHealth; } private set { _maxHealth = value; } }
    public Action OnHited;
    public Action OnDied;

    private int _health;
    private int _maxHealth;

    public PlayerHealth(int startHealth)
    {
        MaxHealth = startHealth;
    }

    public void TakeHealth(int health)
    {
        health = Mathf.Abs(health);

        Health = health + Health > MaxHealth ? MaxHealth : health + Health;
    }

    public void ChangeMaxHealth(int maxHealth)
    {
        maxHealth = maxHealth > 0 ? Mathf.Abs(maxHealth) : 1;
        MaxHealth = maxHealth;
        if (Health > MaxHealth) { Health = MaxHealth; }
    }

    public void TakeDamage()
    {
        Health -= 1;
        Debug.Log(Health);
        if (Health <= 0)
        {
            Debug.Log(" Я мертв ");
            Die();
        }
        OnHited?.Invoke();
    }

    public void FullHealth()
    {
        Health = MaxHealth;
    }

    private void Die()
    {
        OnDied?.Invoke();
    }
}
