using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private readonly Vector2 right = new Vector2(2.5f, -1.5f);
    private readonly Vector2 left = new Vector2(-2.5f, 1.5f);
    private readonly Vector2 up = new Vector2(2.5f, 1.5f);
    private readonly Vector2 down = new Vector2(-2.5f, -1.5f);

    private Rigidbody2D rb;
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 move = Vector2.zero;

        if (inputX > 0) move += right;
        else if (inputX < 0) move += left;

        if (inputY > 0) move += up;
        else if (inputY < 0) move += down;

        if (move.magnitude > 0)
            movement = move.normalized * moveSpeed;
        else
            movement = Vector2.zero;
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            Vector2 newPos = rb.position + movement * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }
    }
}
