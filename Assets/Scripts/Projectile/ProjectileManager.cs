using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    ////ShootSpeed
	public float NormalShootSpeed,MiddleShootSpeed,PowerfulShootSpeed; 
	////RecoilForce
	public float NormalRecoilForce,MiddleRecoilForce,PowerfulRecoilForce;
	////BulletSize
	public float NormalShootSize,MiddleShootSize,PowerfulShootSize;
    public GameObject normalParticle,middleParticle,powerfulParticle,freezingParticle;
    public float lifeTime;
    public float powerfulProjectileExplosionForce;
    public ProjectilesPacket packet;
    public void NormalShoot( Vector2 mouthPos, Vector2 dir )
	{
        Projectile projectile = Instantiate(packet.normalProjectile,mouthPos,Quaternion.identity).GetComponent<Projectile>();
        projectile.SetProjectile(dir);
	}
    public void MiddleShoot( Vector2 mouthPos, Vector2 dir )
	{
        Projectile projectile = Instantiate(packet.middleProjectile,mouthPos,Quaternion.identity).GetComponent<Projectile>();
        projectile.SetProjectile(dir);
	}
    public void PowerfulShoot( Vector2 mouthPos, Vector2 dir )
	{
        Projectile projectile = Instantiate(packet.powerfulProjectile,mouthPos,Quaternion.identity).GetComponent<Projectile>();
        projectile.SetProjectile(dir);
	}
}
