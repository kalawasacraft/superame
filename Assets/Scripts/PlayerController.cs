using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed = 2.5f;
    public float jumpForce = 4f;
    public float longIdleTime = 5f;
    public float attackTwoTime = 0.5f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    public Transform effectPoint;
    public GameObject attackEffect;
    public GameObject healthEffect;
    public GameObject shieldEffect;

    // References
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;

    // Long Idle
    private float _longIdleTimer = 0f;

    // Movement
    private Vector2 _movement;
    private bool _facingRight = true;
    private bool _isGrounded = false;

    // Attack
    private bool _isAttacking = false;
    private int _numberCurrentAttack = 0;
    private bool _intervalTwoAttack = false;
    private float _attackTwoTimer = 0f;

    private bool _isDeath = false;

    void Awake()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (!_isDeath) {
            if (!_isAttacking) {
                // Movement
                float horizontalInput = Input.GetAxisRaw("Horizontal");
                _movement = new Vector2(horizontalInput, 0f);

                if ((horizontalInput < 0f && _facingRight) || (horizontalInput > 0f && !_facingRight)) {
                    Flip();
                }
            }

            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if (Input.GetButtonDown("Jump") && _isGrounded && !_isAttacking) {
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            
            if (Input.GetButtonDown("Fire1") && _isGrounded && !_isAttacking) {
                _movement = Vector2.zero;
                _rigidbody.velocity = Vector2.zero;

                if (_numberCurrentAttack == 0) {
                    _animator.SetTrigger("AttackOne");
                } else if (_numberCurrentAttack == 1) {
                    _animator.SetTrigger("AttackTwo");
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!_isDeath && !_isAttacking && GameManager.IsInputMovement()) {
            float horizontalVelocity = _movement.normalized.x * speed;
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
        } else if (_isDeath) {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    void LateUpdate()
    {
        if (!_isDeath) {
            _animator.SetBool("Idle", _movement == Vector2.zero);
            _animator.SetBool("IsGrounded", _isGrounded);
            _animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);

            if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
                _longIdleTimer += Time.deltaTime;
                
                if (_longIdleTimer >= longIdleTime) {
                    _animator.SetTrigger("LongIdle");
                }
            } else {
                _longIdleTimer = 0f;
            }

            if (_intervalTwoAttack) {
                _attackTwoTimer += Time.deltaTime;
                if (_attackTwoTimer <= attackTwoTime) {
                    _numberCurrentAttack = 1;
                } else {
                    _numberCurrentAttack = 0;
                    _intervalTwoAttack = false;
                }
            }

            if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
                _isAttacking = true;
            } else {
                _isAttacking = false;
            }
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
    }

    public void StartIntervalTwoAttack()
    {
        _intervalTwoAttack = true;
        _attackTwoTimer = 0;
    }

    public void TakeHit(float damage) 
    {
        if (!_isDeath && !GameManager.IsFullShield()) {
            GameManager.UpdateHealth((int)damage);
            int health = GameManager.GetHealth();

            _sprite.color = new Color(1f, 0.5f, 0.5f, 1f);

            Invoke("refreshColor", 0.3f);
            if (health <= 0) {
                GameManager.PlayerDeath();
                _isDeath = true;
                _animator.SetTrigger("Death");

                Invoke("AfterDeath", 2f);
            }
        }
    }

    public void InitEffectAttack(float time)
    {
        GameObject childEffect = Instantiate(attackEffect) as GameObject;
        childEffect.transform.parent = transform;
        childEffect.transform.position = effectPoint.position;
        Destroy(childEffect, time);
    }

    public void InitEffectShield(float time)
    {
        GameObject childEffect = Instantiate(shieldEffect) as GameObject;
        childEffect.transform.parent = transform;
        childEffect.transform.position = effectPoint.position;
        Destroy(childEffect, time);
    }

    public void InitEffectHealth(float time)
    {
        GameObject childEffect = Instantiate(healthEffect) as GameObject;
        childEffect.transform.parent = transform;
        childEffect.transform.position = effectPoint.position;
        Destroy(childEffect, time);
    }

    public void AfterDeath()
    {
        GameManager.GameOver();   
    }

    public void refreshColor()
    {
        _sprite.color = new Color(1f, 1f, 1f, 1f);
    }
}
