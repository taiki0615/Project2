using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Object.FindFirstObjectByType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ShowGameOverUI();
    }
    private void ShowGameOverUI()
    {
        if (player != null) return;
        gameOverUI.SetActive(true);
    }
}
