using UnityEngine;
using UnityEngine.InputSystem;  // 新InputSystemの名前空間！
using System.Collections;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool bjump;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private int hp;
    [SerializeField] private float damageTime;
    [SerializeField] private float flashTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        bjump = false;
    }
    void Update()
    {
        Debug.Log(hp);
    }
    void FixedUpdate()
    {
        Move();
        LookMoveDirec();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    private void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        anim.SetBool("Walk", moveInput.x != 0.0f);
    }
    private void LookMoveDirec()
    {
        if(moveInput.x > 0.0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if(moveInput.x < 0.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            bjump = false;
            anim.SetBool("Jump", bjump);
        }
        if (collision.gameObject.tag == "enemy")
        {
            HitEnemy(collision.gameObject);
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        }
    }
    private void HitEnemy(GameObject enemy)
    {
        float halfScaleY = transform.lossyScale.y / 2.0f;
        float enemyHalfScaleY = enemy.transform.lossyScale.y / 2.0f;
        if (transform.position.y - (halfScaleY - 0.1f) >= enemy.transform.position.y + (enemyHalfScaleY - 0.1f))
        {
            Destroy(enemy);
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

        }
        else
        {
            enemy.GetComponent<Enemy>().PlayerDamage(this);
            StartCoroutine(Damage());
        }
    }
    IEnumerator Damage()
    {
        Color color = spriteRenderer.color;
        for (int i = 0; i < damageTime; i++)
        {
            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 0.0f);

            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);
        }
        spriteRenderer.color = color;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    private void Dead()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || bjump) return;
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        bjump = true;
        anim.SetBool("Jump", bjump);
    }
    public void Damage(int damage)
    {
        hp = Mathf.Max(hp - damage, 0);
        Dead();
    }
    public int GetHP()
    {
        return hp;
    }

}
