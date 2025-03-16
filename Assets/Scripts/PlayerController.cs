using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{

    public float walkSpeed=5f;

    Rigidbody2D rb;
    Vector2 moveInput;

    public bool isMoving { get; private set; }



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * walkSpeed , rb.linearVelocityY);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput= context.ReadValue<Vector2>();

        isMoving = moveInput != Vector2.zero;



    }
}
