using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] int attakPower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        rb.linearVelocity = new Vector2(Vector2.left.x * moveSpeed, rb.linearVelocity.y);
    }
    public void PlayerDamage(Player player)
    {
        player.Damage(attakPower);
    }
}
