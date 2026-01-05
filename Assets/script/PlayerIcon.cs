using UnityEngine;
using UnityEngine.UI;

public class PlayerIcon : MonoBehaviour
{
    [SerializeField] private GameObject playerIcon;

    private Player player;
    private int beforeHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Object.FindFirstObjectByType<Player>();
        beforeHP = player.GetHP();
        CreateHPIcon();
    }

    private void CreateHPIcon()
    {
        for(int i = 0; i < player.GetHP(); i++)
        {
            GameObject playerHPObj = Instantiate(playerIcon, transform, false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        ShowHPIcon();
    }
    private void ShowHPIcon()
    {
        if (beforeHP == player.GetHP()) return;

        Image[] icons = transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < player.GetHP());
        }
        beforeHP = player.GetHP();
    }
}
