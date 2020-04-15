using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int killedEnemyCount;
	public float currentHealth;
	public int maxHealth;
	public bool isThereActivePowerUp,GetDataFromBefore,dontGetDamage = false;
	public int howManyRoomVisited=0;
	PermanentPowerUpController passivePowerUps;
	[SerializeField] Player_Shoot player_Shoot;
	[SerializeField] GameUI gameUI;
	
	void Start () 
	{
		passivePowerUps = GetComponent<PermanentPowerUpController>();
		currentHealth = maxHealth;
	}

	
	public void GetDamage(float damage)
	{
		if(dontGetDamage == true)
		 	return;

		currentHealth -= damage;
		if(currentHealth >100)
			currentHealth = 100;
		else if( currentHealth <0)
		{
			currentHealth = 0;
			if(FindObjectOfType<SurvivalGameManager>() != null)
			{
				FindObjectOfType<SurvivalGameManager>().EndGame();
			}
		}
		gameUI.UpdateHealthBar(currentHealth);
	}
	public void AddEnemyKilled()
	{
		killedEnemyCount++;
		if(passivePowerUps.lifeSteal)
		{
			GainHealth(5);
			gameUI.UpdateHealthBar(currentHealth);
		}
	}
	public void GainHealth(int gain)
	{
		currentHealth += gain;

		if(currentHealth >100)
			currentHealth = 100;
		else if( currentHealth <0)
			currentHealth = 0;

		gameUI.UpdateHealthBar(currentHealth);
	}
	
}
