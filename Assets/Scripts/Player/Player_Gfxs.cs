using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Gfxs : MonoBehaviour
{
    public SpriteRenderer energyGlass;
    Player_Shoot player_Shoot;
    public Color normal,middle,powerfull;
    void Start()
    {
        player_Shoot = GetComponent<Player_Shoot>();
    }
    IEnumerator SetEnergyGlass()
    {
        float t = 0.0f;
        Color startColor = energyGlass.GetComponent<MeshRenderer>().material.color;
        while(t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / 1);
            if(player_Shoot.state == State.NormalShot)
            {
                energyGlass.GetComponentInChildren<MeshRenderer>().material.color 
                    = Color.Lerp(energyGlass.GetComponentInChildren<MeshRenderer>().material.color, normal, t);
            }
            else if(player_Shoot.state == State.MiddleShot)
            {
                energyGlass.GetComponentInChildren<MeshRenderer>().material.color 
                    = Color.Lerp(energyGlass.GetComponentInChildren<MeshRenderer>().material.color, middle, t);
            }
            else if(player_Shoot.state == State.PowerfulShoot)
            {
                energyGlass.GetComponentInChildren<MeshRenderer>().material.color 
                    = Color.Lerp(energyGlass.GetComponentInChildren<MeshRenderer>().material.color, powerfull, t);
            }
            yield return 0;
        }
    }
    public IEnumerator lerpColor(GameObject _gameObject, Color _targetColor)
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            Color currentColor = _gameObject.GetComponentInChildren<MeshRenderer>().material.color;
            t += Time.deltaTime * (Time.timeScale / 1);
            _gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(_gameObject.GetComponentInChildren<MeshRenderer>().material.color, _targetColor, t);
            yield return 0;
        }
    }
}
