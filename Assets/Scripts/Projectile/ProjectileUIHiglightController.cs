using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ProjectileUIHiglightController : MonoBehaviour
{
    [SerializeField] Image background, normal,middle,powerful;
    [SerializeField] TextMeshProUGUI priceText;
    public void Set(Color c, Sprite n, Sprite m, Sprite p, int price)
    {
        background.color = c;
        normal.sprite = n;
        middle.sprite = m;
        powerful.sprite = p;
        priceText.text = price.ToString();
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
