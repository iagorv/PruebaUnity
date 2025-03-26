using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;

    Animator animator;



    [SerializeField]
    private int _maxHealth = 100;




    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        private set
        {
            _maxHealth = value;
        }
    }


    [SerializeField]
    private int _health = 100;


    public int Health
    {
        get
        {
            return _health;
        }
        private set
        {
            _health = value;

            if (_health <= 0)
            {
                IsAlive = false;
            }

        }
    }


    [SerializeField]

    private bool _isAlive = true;
    [SerializeField]
    private bool isInvencible = false;
    public bool IsHit{ get
    {
        return animator.GetBool(AnimationStrings.isHit);
    }
    private set{
        animator.SetBool(AnimationStrings.isHit, value);
    } }
    private float timeSinceHit = 0;
    public float invencibleTime = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set: " + value);
        }
    }




    private void Awake()
    {
        animator = GetComponent<Animator>();

    }


    private void Update()
    {
        if (isInvencible)
        {
            if (timeSinceHit > invencibleTime)
            {
                isInvencible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;

        }
    }


    public bool Hit(int damage, Vector2 knockback)
    {

        if (IsAlive && !isInvencible)
        {
            Health -= damage;
            isInvencible = true;
            IsHit=true;
            damageableHit?.Invoke(damage, knockback);

            return true;
        }
        return false;

    }


}
