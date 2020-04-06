using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class EntranceUI : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI coinText;
    void Start()
    {
        UpdateCoin();
    }
    public void UpdateCoin()
    {
        coinText.text = SaveAndLoadGameData.instance.savedData.coin.ToString();
    }
    public void PlaySurvival()
    {
        SceneManager.LoadScene(1);
    }
}
