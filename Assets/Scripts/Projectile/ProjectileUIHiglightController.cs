using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ProjectileUIHiglightController : MonoBehaviour
{
    [SerializeField] Image background, normal,middle,powerful;
    public void Set(Color c, Sprite n, Sprite m, Sprite p)
    {
        background.color = c;
        normal.sprite = n;
        middle.sprite = m;
        powerful.sprite = p;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
