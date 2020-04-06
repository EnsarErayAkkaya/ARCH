using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int killedEnemyCount;
	public float currentHealth;
	public int maxHealth;
	public bool isThereActivePowerUp,GetDataFromBefore;
	public int howManyRoomVisited=0;
	PermanentPowerUpController passivePowerUps;
	
	void Start () 
	{
		passivePowerUps = GetComponent<PermanentPowerUpController>();
		if(GetDataFromBefore == true)
		{
/* 			GetSavedData();
 */		}
		else{
			currentHealth = maxHealth;
		}
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Wall"))
		{
			if(other.gameObject.GetComponent<ReflectorWall>() != null)
			{
				other.gameObject.GetComponent<Player_Shoot>().recoiledVector =  other.gameObject.transform.position;
			}
        }
	}
	
	public void GetDamage(float damage)
	{
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
		FindObjectOfType<GameUI>().UpdateHealthBar(currentHealth);
	}
	public void AddEnemyKilled()
	{
		killedEnemyCount++;
		if(passivePowerUps.lifeSteal)
		{
			GainHealth(5);
			FindObjectOfType<GameUI>().UpdateHealthBar(currentHealth);
		}
	}
	public void GainHealth(int gain)
	{
		currentHealth += gain;

		if(currentHealth >100)
			currentHealth = 100;
		else if( currentHealth <0)
			currentHealth = 0;

		FindObjectOfType<GameUI>().UpdateHealthBar(currentHealth);
	}
	
}
