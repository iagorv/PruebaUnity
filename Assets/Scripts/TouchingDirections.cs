using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    [SerializeField] public float coyoteTime = 0.2f; // Tiempo que permite el salto después de caer
    public float lastGroundedTime; // Último momento en que estuvo en el suelo





    CapsuleCollider2D touchingCol;
    Animator animator;
    RaycastHit2D[] groundhits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];

    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];



    [SerializeField]
    private bool _isGrounded;





    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);


        }
    }

    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.IsOnWall, value);


        }
    }

    [SerializeField]
    private bool _isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.IsOnCeiling, value);


        }
    }


    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundhits, groundDistance) > 0;

        if (IsGrounded)
        {
            lastGroundedTime = Time.time; // Guarda el tiempo actual si está en el suelo


            PlayerController player = GetComponent<PlayerController>();
            if (player != null)
            {
                player.numSaltos = 0; // Solo se ejecuta si hay un PlayerController
                player.wallJump=false;
            }


            


        }


        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;

        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;


    }




}
