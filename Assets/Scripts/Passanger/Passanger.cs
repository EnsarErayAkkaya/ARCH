using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Passanger 
{
    public int currentHealth;
    public PassangerHealth passangerHealth; 
    public Passanger(int _currentHealth)
    {
        currentHealth = _currentHealth;
        SetPlayerHealth();
    }
    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth < 1)
            currentHealth = 0;
        SetPlayerHealth();
    }
    public void GetHeal(int heal)
    {
        currentHealth += heal;
        if(currentHealth > 7)
            currentHealth = 7;
        SetPlayerHealth();
    }
    void SetPlayerHealth()
    {
        if(currentHealth<1)
            passangerHealth = PassangerHealth.dead;
        else if(currentHealth >=1 && currentHealth<=3)
            passangerHealth = PassangerHealth.bad;
        else if(currentHealth>3 && currentHealth<=5)
            passangerHealth = PassangerHealth.normal;
        else if(currentHealth>5 && currentHealth<=7)
            passangerHealth = PassangerHealth.good;
    }
}
public enum PassangerHealth
{
    dead,bad,normal,good,none
}
