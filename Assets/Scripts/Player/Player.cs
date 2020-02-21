using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int killedEnemyCount;
	public int currentHealth;
	public int maxHealth;
	public bool isThereActivePowerUp,GetDataFromBefore;
	public int howManyRoomVisited=0;
	PermanentPowerUpController passivePowerUps;
	
	void Start () 
	{
		passivePowerUps = GetComponent<PermanentPowerUpController>();
		if(GetDataFromBefore == true)
		{
			GetSavedData();
		}
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
	
	public void GetDamage(int damage)
	{
		currentHealth -= damage;
		if(currentHealth >100)
			currentHealth = 100;
		else if( currentHealth <0)
			currentHealth = 0;
		//if(FindObjectOfType<SurvivalGameManager>() == null)
		FindObjectOfType<GameUI>().UpdateHealthBar(currentHealth);

	}
	public void AddEnemyKilled()
	{
		killedEnemyCount++;
		if(passivePowerUps.lifeSteal)
		{
			GainHealth(5);
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
	//Call one off active powerUps
	public void UsePowerUp(int powerUpIndex)
	{
		PowerUpManager pm = FindObjectOfType<PowerUpManager>();
		pm.GivePower(gameObject,pm.selectedActivePowerUps[powerUpIndex].powerUpType);
	}
	
	private void GetSavedData()
	{
		//Gets the last data saved
		
		PlayerData saved = SaveAndLoadGameData.instance.savedData.playerData;

		currentHealth = saved.currentHealth;
		maxHealth =saved.maxHealth;
		howManyRoomVisited = saved.howManyRoomVisited;
		killedEnemyCount = saved.killedEnemyCount;
		transform.position = new Vector2(saved.positionX,saved.positionY);
	}
}
