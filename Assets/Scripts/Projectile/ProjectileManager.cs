using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance;
    ////ShootSpeed
	public float NormalShootSpeed,MiddleShootSpeed,PowerfulShootSpeed; 
	////RecoilForce
	public float NormalRecoilForce,MiddleRecoilForce,PowerfulRecoilForce;
	////BulletSize
	public float NormalShootSize,MiddleShootSize,PowerfulShootSize;
    public GameObject normalParticle,middleParticle,powerfulParticle,freezingParticle;
    public float lifeTime;
    public float powerfulProjectileExplosionForce;
    public List<ProjectilesPacket> allPackets;
    public List<PacketType> ownedPackets;
    public PacketType choosedPacketType;
    ProjectilesPacket choosedPacket;
    void Awake()
    {
        if (ProjectileManager.instance == null)
        {
            ProjectileManager.instance = this;
        }
        else if (ProjectileManager.instance != null)
        {
            Destroy(ProjectileManager.instance.gameObject);
            ProjectileManager.instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        //Kaydedilenleri geri al
        choosedPacketType = SaveAndLoadGameData.instance.savedData.choosedPacketType;
        ownedPackets = SaveAndLoadGameData.instance.savedData.ownedPackets;
        if(ownedPackets.Any(s => s == PacketType.Default) == false)
        {
            ownedPackets.Add(PacketType.Default);
        }

        choosedPacket = allPackets.FirstOrDefault(s => s.packet == choosedPacketType);
    }
   
    public void NormalShoot( Vector2 mouthPos, Vector2 dir )
	{
        Projectile projectile = Instantiate(choosedPacket.normalProjectile,mouthPos,Quaternion.identity).GetComponent<Projectile>();
        projectile.SetProjectile(dir);
	}
    public void MiddleShoot( Vector2 mouthPos, Vector2 dir )
	{
        Projectile projectile = Instantiate(choosedPacket.middleProjectile,mouthPos,Quaternion.identity).GetComponent<Projectile>();
        projectile.SetProjectile(dir);
	}
    public void PowerfulShoot( Vector2 mouthPos, Vector2 dir )
	{
        Projectile projectile = Instantiate(choosedPacket.powerfulProjectile,mouthPos,Quaternion.identity).GetComponent<Projectile>();
        projectile.SetProjectile(dir);
	}
    public void OwnPacket(PacketType packet)
    {
        ownedPackets.Add(packet);
        SaveAndLoadGameData.instance.savedData.ownedPackets = ownedPackets;
        SaveAndLoadGameData.instance.Save();
    }
    public void SelectPacket(PacketType packet)
    {
        if(choosedPacketType == PacketType.None)
        {
            choosedPacketType = packet;
            choosedPacket = allPackets.FirstOrDefault(s => s.packet == choosedPacketType);
            SaveChoosedPacket();
        }
        else
        {
            Debug.Log("A Packet is already choosed. You need to deselect it to select an other one.");
        }
    }
    public void SaveChoosedPacket()
    {
        SaveAndLoadGameData.instance.savedData.choosedPacketType = choosedPacketType;
        SaveAndLoadGameData.instance.Save();
    }
    public void DeselectPacket()
    {
        choosedPacketType = PacketType.None;
        choosedPacket = null;
        SaveChoosedPacket();
    }
    public void SelectAuto()
    {
        if(choosedPacketType == PacketType.None)
        {
            choosedPacketType = PacketType.Default;
            choosedPacket = allPackets.FirstOrDefault(s => s.packet == choosedPacketType);
            SaveChoosedPacket();
        }
    }
}
