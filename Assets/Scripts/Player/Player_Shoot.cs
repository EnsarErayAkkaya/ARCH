using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Shoot : MonoBehaviour {

  	////ShootSpeed
	public float NormalShootSpeed,MiddleShootSpeed,PowerfulShootSpeed; 
	////RecoilForce
	public float NormalRecoilForce,MiddleRecoilForce,PowerfulRecoilForce;
	////TimeLimits
	public float NormalShootTimeLimit,MiddleShootTimeLimit,PowerfulShootTimeLimit;
	////BulletSize
	public float NormalShootSize,MiddleShootSize,PowerfulShootSize;

	public bool canRecoil;
	
	public Vector2 recoiledVector;
	private float LastShootTime, chargedTime;

    public bool ShootCharging,canShoot;

    public GameObject normalProjectile,middleProjectile,powerfulProjectile;
	public State state;
	public Transform mouthFrontPos;
	public GameObject shootingParticle;
	private Rigidbody2D rb;
	public float recoilForce, speedLimit;
	public bool recoilCall;
	Player_Gfxs gfxs;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		canShoot = true;
		gfxs = GetComponent<Player_Gfxs>();
	}

	
	void Update () {
		if(canShoot)
		{
			if(EventSystem.current.IsPointerOverGameObject())
				return;

			if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
			{
				Look();
				Shoot();
			}
			else if(Application.platform == RuntimePlatform.Android)
			{
				LookAndroid();
				ShootAndroid();
			}
		}
	}
	void FixedUpdate()
	{
		if(recoilCall)
		{
			recoilCall = false;
			Recoil();
		}
		LimitSpeed();
	}
	
	void LookAndroid()
	{
		if(Input.touchCount < 1)
			return;
		Touch touch = Input.GetTouch(0);
		if(/* Input.GetMouseButton(0) || */ touch.phase == TouchPhase.Moved )
		{
			var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
			var angle =  (Mathf.Atan2(dir.y,dir.x)* Mathf.Rad2Deg)%360;
			transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
		}
	}
	void ShootAndroid()
	{
		Touch touch= Input.GetTouch(0);;
		if (Application.platform == RuntimePlatform.Android)
        {
            if(Input.touchCount < 1)
			return;
        }
		
		if(/* Input.GetMouseButtonDown(0) ||*/   touch.phase == TouchPhase.Began )
		{	
			chargedTime = Time.time;
			ShootCharging = true;
			SetShootingParticle(true);
			//Arkadaki cam küreyi doldurur
			gfxs.CallSetEnergyGlass();
		}

		if( ( /*Input.GetMouseButtonUp(0)  ||  */touch.phase == TouchPhase.Ended )  && ShootCharging && Time.time>LastShootTime+NormalShootTimeLimit)
		{
			ShootCharging = false;
			gfxs.CallSetEnergyGlassToNormal();
			if( GetComponent<Player>().isThereActivePowerUp == false )
			{
				canRecoil = true;
			}
			
			Vector3 mouthPos = mouthFrontPos.position;

			//With space
			Vector3 target = mouthPos;		

			//With mouse
			//Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Vector2 Direction = target - transform.position;

			if( Time.time - chargedTime < MiddleShootTimeLimit )
			{
				var projectile = Instantiate(normalProjectile,mouthPos,Quaternion.identity);
				FindObjectOfType<AudioManager>().Play( "NormalShot" );
				NormalShoot(projectile,Direction.normalized);
				
			}
			else if( Time.time - chargedTime >= MiddleShootTimeLimit && Time.time - chargedTime < PowerfulShootTimeLimit )
			{
				var projectile = Instantiate(middleProjectile,mouthPos,Quaternion.identity);
				MiddleShoot(projectile,Direction.normalized);
			}
			else if( Time.time - chargedTime >= PowerfulShootTimeLimit && Time.time - chargedTime < PowerfulShootTimeLimit + 2f )
			{
				var projectile = Instantiate(powerfulProjectile,mouthPos,Quaternion.identity);
				PowerfulShoot(projectile,Direction.normalized);
			}
			SetShootingParticle(false);
		}
		if(Time.time - chargedTime >= PowerfulShootTimeLimit + 2f && ShootCharging)
		{
			SetShootingParticle(false);
		}
	}
	void Look()
	{
		if( Input.GetMouseButton(0) )
		{
			var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
			var angle =  (Mathf.Atan2(dir.y,dir.x)* Mathf.Rad2Deg)%360;
			transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
		}
	}
	void Shoot()
	{		
		if( Input.GetMouseButtonDown(0) )
		{	
			chargedTime = Time.time;
			ShootCharging = true;
			SetShootingParticle(true);
			//Arkadaki cam küreyi doldurur
			gfxs.CallSetEnergyGlass();
		}

		if( ( Input.GetMouseButtonUp(0)  )  && ShootCharging && Time.time>LastShootTime+NormalShootTimeLimit)
		{
			ShootCharging = false;
			gfxs.CallSetEnergyGlassToNormal();
			if( GetComponent<Player>().isThereActivePowerUp == false )
			{
				canRecoil = true;
			}
			
			Vector3 mouthPos = mouthFrontPos.position;

			//With space
			Vector3 target = mouthPos;		

			//With mouse
			//Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Vector2 Direction = target - transform.position;

			if( Time.time - chargedTime < MiddleShootTimeLimit )
			{
				var projectile = Instantiate(normalProjectile,mouthPos,Quaternion.identity);
				FindObjectOfType<AudioManager>().Play( "NormalShot" );
				NormalShoot(projectile,Direction.normalized);
				
			}
			else if( Time.time - chargedTime >= MiddleShootTimeLimit && Time.time - chargedTime < PowerfulShootTimeLimit )
			{
				var projectile = Instantiate(middleProjectile,mouthPos,Quaternion.identity);
				MiddleShoot(projectile,Direction.normalized);
			}
			else if( Time.time - chargedTime >= PowerfulShootTimeLimit && Time.time - chargedTime < PowerfulShootTimeLimit + 2f )
			{
				var projectile = Instantiate(powerfulProjectile,mouthPos,Quaternion.identity);
				PowerfulShoot(projectile,Direction.normalized);
			}
			SetShootingParticle(false);
		}
		if(Time.time - chargedTime >= PowerfulShootTimeLimit + 2f && ShootCharging)
		{
			SetShootingParticle(false);
		}
	}
	void NormalShoot( GameObject projectile,Vector2 ShootVelocity )
	{
		recoiledVector = -ShootVelocity * NormalRecoilForce;///Recoil Force
			
		projectile.GetComponent<Rigidbody2D>().velocity = ShootVelocity * NormalShootSpeed;///Speed

		projectile.transform.localScale *= NormalShootSize; ///Size

		LastShootTime = Time.time;
		state = State.NormalShot;
		recoilCall = true;
	}
	void MiddleShoot(  GameObject projectile,Vector2 ShootVelocity )
	{
		recoiledVector = -ShootVelocity * MiddleRecoilForce;///Recoil Force
			
		projectile.GetComponent<Rigidbody2D>().velocity = ShootVelocity * MiddleShootSpeed;///Speed

		projectile.transform.localScale *= MiddleShootSize; ///Size

		LastShootTime = Time.time;
		state = State.MiddleShot;
		recoilCall = true;
	}
	void PowerfulShoot(  GameObject projectile,Vector2 ShootVelocity )
	{
		recoiledVector = -ShootVelocity * PowerfulRecoilForce;///Recoil Force
			
		projectile.GetComponent<Rigidbody2D>().velocity = ShootVelocity * PowerfulShootSpeed;///Speed

		projectile.transform.localScale *= PowerfulShootSize; ///Size

		FindObjectOfType<Camera_Shake>().Call_Shake(.7f,.2f);///Shakes Camera

		LastShootTime = Time.time;
		state = State.PowerfulShoot;
		recoilCall = true;
	}
	public void Recoil()
	{
		if(canRecoil)
		{
			rb.AddForce(recoiledVector * recoilForce,ForceMode2D.Impulse);
			//transform.position =  Vector3.Lerp(transform.position, recoiledVector, Time.deltaTime);
		}
		IsStatic();
	}
	private void LimitSpeed()
	{
		if(rb.velocity.magnitude > speedLimit )
		{
			if(rb.velocity.x > 0)
			{
				if(rb.velocity.y > 0)
				{
					rb.velocity = new Vector2(rb.velocity.x - .1f, rb.velocity.y - .1f);
				} 
				else if(rb.velocity.y < 0)
				{
					rb.velocity = new Vector2(rb.velocity.x - .1f, rb.velocity.y + .1f);
				}
			}
			else if(rb.velocity.x < 0)
			{
				if(rb.velocity.y > 0)
				{
					rb.velocity = new Vector2(rb.velocity.x + .1f, rb.velocity.y - .1f);
				} 
				else if(rb.velocity.y < 0)
				{
					rb.velocity = new Vector2(rb.velocity.x + .1f, rb.velocity.y + .1f);
				}
			}
		}
	}
	void IsStatic()
	{
		if( rb.velocity.magnitude < 0.15f )
		{
			state = State.Static;
		}
	}
	public void SetShootingParticle(bool isActive)
	{
		if(isActive)
		{
			shootingParticle.SetActive(true);
			var part = shootingParticle.GetComponent<ParticleSystem>();
			part.Play();
		}
		else
		{
			shootingParticle.SetActive(false);
			var part = shootingParticle.GetComponent<ParticleSystem>();
			part.Stop();
		}
	}

}
public enum State////player moving or stoped or something else
{
	Static,NormalShot,MiddleShot,PowerfulShoot
}
