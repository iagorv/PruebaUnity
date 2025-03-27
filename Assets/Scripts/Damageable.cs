using System;
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

   public bool LockVelocity
    {
        get
        {

            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
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

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity= true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.CharacterDamaged?.Invoke(gameObject, damage);

            return true;
        }
        return false;

    }

    public void Heal(int healthRestore)
    {
        if (IsAlive)
        {
            int maxHeal=Mathf.Max( MaxHealth - Health,0);
            int actualHealth =  Mathf.Min(maxHeal,healthRestore);

            Health +=actualHealth;
            CharacterEvents.CharacterHealed(gameObject, actualHealth);
          
        }
    }


}
