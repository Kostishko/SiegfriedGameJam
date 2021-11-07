using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterFight : MonoBehaviour
{
    //[SerializeField] float _attackOffset = 5f;
    [SerializeField] float _meleeTimeBetweenAttacks = 1f;
    [SerializeField] private GameObject _plume;

    private MachineGun _machineGun;
    private float _timeSinceMeleeAttack;
    private CharacterState _charState;
    private Animator _animator;
    private readonly int _animAtack = Animator.StringToHash("MeleeAtack");
    private readonly int _animAtackUp = Animator.StringToHash("MeleeAtackUp");
    private readonly int _animAtackDown = Animator.StringToHash("MeleeAtackDown");
    private readonly int _animAtackLeft = Animator.StringToHash("MeleeAtackLeft");
    private readonly int _animAtackRight = Animator.StringToHash("MeleeAtackRight");
    private Vector3 _attackDir;

    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip swordSound;

    private void Start()
    {
        _machineGun = GetComponentInChildren<MachineGun>();
        _charState = GetComponent<CharacterState>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_charState.state == CharacterStates.Dead) return;
        _timeSinceMeleeAttack += Time.deltaTime;

        HandleAttack();
    }
    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && _timeSinceMeleeAttack > _meleeTimeBetweenAttacks)
        {
            _timeSinceMeleeAttack = 0f;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _attackDir = (mousePosition - transform.position).normalized;

            var animTrigger = _animAtackDown;
            if (Mathf.Abs(_attackDir.y) > Mathf.Abs(_attackDir.x))//vertical
            {
                if (_attackDir.y > 0)
                    animTrigger = _animAtackUp;
            }
            else //horizontal
            {
                if (_attackDir.x > 0)
                    animTrigger = _animAtackRight;
                else
                    animTrigger = _animAtackLeft;
            }

            _animator.SetTrigger(animTrigger);
            _charState.state = CharacterStates.MeleeAttack;
            _machineGun.gameObject.SetActive(false);
            CreatePlumb(animTrigger == _animAtackLeft || animTrigger == _animAtackUp && _attackDir.x < 0);

            audioSource.PlayOneShot(swordSound);
        }
        if (Input.GetMouseButtonDown(1) && _charState.state != CharacterStates.MeleeAttack)
        {
            Shoot();
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void Shoot()
    {
        _machineGun.Shoot();
    }

    private void OnEnterAttackPlume(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(_charState.meleeDamage);
        }
    }

    #region Animation events
    public void EndMeleeAtack()//Animation event
    {
        _charState.state = CharacterStates.Normal;
        _machineGun.gameObject.SetActive(true);
    }
    #endregion

    private void CreatePlumb(bool isLeft)
    {
        float alpha = _attackDir.y < 0 && Mathf.Abs(_attackDir.y) > Mathf.Abs(_attackDir.x) ? 0f : 1f;

        var plumeTransform = Instantiate(_plume, transform.position, Quaternion.identity, transform).GetComponent<Transform>();
        plumeTransform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(_attackDir.y, _attackDir.x) * Mathf.Rad2Deg);

        if (isLeft)
            plumeTransform.localScale = new Vector3(1, -1, 1);
        plumeTransform.GetComponentInChildren<Plume>().OnEnter += OnEnterAttackPlume;
        plumeTransform.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        Destroy(plumeTransform.gameObject, 0.2f);
    }
}
