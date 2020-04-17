using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicUIManager : MonoBehaviour
{
    [SerializeField] Button off,on;
    [SerializeField] string musicName;
    void Start()
    {
        if(SaveAndLoadGameData.instance.savedData.isMusicPlaying)
        {
            UnmuteMusic();
        }
        else
        {
            MuteMusic();
        }

        if(AudioManager.instance.IsMuted(musicName) == true)
        {
            off.gameObject.SetActive(true);
            on.gameObject.SetActive(false);
        }
        else
        {
            off.gameObject.SetActive(false);
            on.gameObject.SetActive(true);
        }
    }
    public void MuteMusic( )
    {
        AudioManager.instance.Mute(musicName);
        SaveMusicSetting(false);
    }
    public void UnmuteMusic()
    {
        AudioManager.instance.Unmute(musicName);
        SaveMusicSetting(true);
    }
    void SaveMusicSetting(bool m)
    {
        SaveAndLoadGameData.instance.savedData.isMusicPlaying = m;
        SaveAndLoadGameData.instance.Save();
    }
}
