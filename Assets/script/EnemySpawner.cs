using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]private GameObject enemy;

    private Player player;
    private GameObject enemyObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Object.FindFirstObjectByType<Player>();
        enemyObj = null;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (player == null) return;

        Vector3 playerPos = player.transform.position;
        Vector3 cameraMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 scale = enemy.transform.lossyScale;

        float distance = Vector2.Distance(transform.position, new Vector2(playerPos.x, transform.position.y));
        float spawnDis = Vector2.Distance(playerPos, new Vector2(cameraMaxPos.x + scale.x / 2.0f, playerPos.y));
        if (distance < spawnDis && enemyObj == null)
        {
            enemyObj = Instantiate(enemy);
            enemyObj.transform.position = transform.position;
            transform.parent = enemyObj.transform;
        }
    }
}
