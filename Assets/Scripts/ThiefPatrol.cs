using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefPatrol : MonoBehaviour
{
    public float maxHitPoints;
    [SerializeField] private float _speed;
    public float decreasedSeconds = 2f;
    public HealthBarEnemy healthBarEnemy;
    public SecondsGained secondsGained;

    private bool _facingRight = true;
    private float _hitPoints;

    [Header("Raycast parameters")]
    [SerializeField] private Transform _groundSensor;
    [SerializeField] private float _groundSensorDistance = 0.35f;

    [Header("OverlapBox parameters")]
    [SerializeField] private Transform _playerSensor;
    [SerializeField] private Vector2 _playerSensorSize = Vector2.one;

    [Header("Gizmos parameters")]
    public bool showGizmos = true;
    public Color gizmoGroundSensorColor = Color.green;
    public Color gizmoplayerSensorColor = Color.blue;

    private bool _isOnEdge = false;
    private bool _isAttacking = false;
    private bool _isDeath = false;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _hitPoints = maxHitPoints;
        healthBarEnemy.SetHealth(_hitPoints, maxHitPoints);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!_isDeath) {
            
            RaycastHit2D groundInfo = Physics2D.Raycast(_groundSensor.position, Vector2.down, _groundSensorDistance, LayerMask.GetMask("Ground"));
            Collider2D collider = Physics2D.OverlapBox((Vector2)_playerSensor.position, _playerSensorSize, 0, LayerMask.GetMask("Player"));

            if (collider != null && !_isAttacking) {

                float distancePlayer = collider.gameObject.transform.position.x - transform.position.x;

                if (Mathf.Abs(distancePlayer) <= 0.5f) {
                    
                    if ((_facingRight && distancePlayer < 0) || !_facingRight && distancePlayer > 0) {
                        Flip();
                    }
                    _animator.SetTrigger("Attack");
                } else {
                    _animator.SetTrigger("Throw");
                }
            }

            if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
                _isAttacking = true;
                _animator.SetBool("Idle", true);
            } else {
                if (_isAttacking) {
                    Invoke("IdleComplete", 0.05f);
                }
                _isAttacking = false; 
            }
    
            if (!_isOnEdge && !_isAttacking && collider == null) {
                if ((groundInfo.collider == null || Mathf.Abs(groundInfo.point.y - _groundSensor.position.y) <= 0.1)) {
                    Flip();
                    _isOnEdge = true;
                    _animator.SetBool("Idle", true);
                    Invoke("IdleComplete", GameManager.RandomNumber(2, 5) * 1f);
                    
                } else {
                    _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
                }   
            }
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        _speed *= -1;
        
        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
    }

    private void IdleComplete()
    {
        _isOnEdge = false;
        _animator.SetBool("Idle", false);
    }

    public void TakeHit(float damage)
    {
        if (!_isDeath) {
            _hitPoints = Mathf.Max(_hitPoints - damage, 0f);
            healthBarEnemy.SetHealth(_hitPoints, maxHitPoints);

            _sprite.color = new Color(1f, 0.5f, 0.5f, 1f);
            Invoke("refreshColor", 0.3f);
            
            if (_hitPoints <= 0) {
                _isDeath = true;
                _animator.SetTrigger("Death");
                secondsGained.Show(transform.position + new Vector3(0f,0.27f,0f), decreasedSeconds);

                TimerController.DecreaseTime(decreasedSeconds);
                Destroy(gameObject, 1.4f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos) {
            Gizmos.color = gizmoGroundSensorColor;
            Gizmos.DrawLine(_groundSensor.transform.position, _groundSensor.transform.position + Vector3.down * _groundSensorDistance);
            Gizmos.color = gizmoplayerSensorColor;
            Gizmos.DrawCube((Vector2)_playerSensor.position, _playerSensorSize);
        }
    }

    public void refreshColor()
    {
        _sprite.color = new Color(1f, 1f, 1f, 1f);
    }
}
