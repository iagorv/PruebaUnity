using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;


    CapsuleCollider2D touchingCol;
    Animator animator;
    RaycastHit2D[] groundhits = new RaycastHit2D[5];


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
    }
}
