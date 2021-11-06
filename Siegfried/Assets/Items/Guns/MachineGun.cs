using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    [SerializeField] private GameObject _bulletPrefab;

    private Transform _aimTransform;
    [SerializeField] private Transform _bulletStartTransform;
    private Camera _camera;
    private Vector3 _mouseDir;
    private int _characterOrder = 10;
    private Transform _characterTransform;
    private SpriteRenderer _renderer;
    private void Awake()
    {
        _camera = Camera.main;
        _aimTransform = transform;
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _characterTransform = transform.parent;
        _characterOrder = _characterTransform.GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void Update()
    {
        UpdateRotation();
        UpdateSorting();
    }

    private void UpdateRotation()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _mouseDir = (mousePosition - transform.position).normalized;

        var localScale = _aimTransform.localScale;
        if (_mouseDir.x < 0)
            localScale.x = -1;
        else
            localScale.x = 1;
        _aimTransform.localScale = localScale;

        float angle = Mathf.Atan2(_mouseDir.y * localScale.x, _mouseDir.x * localScale.x) * Mathf.Rad2Deg;

        _aimTransform.eulerAngles = new Vector3(0, 0, angle);

    }

    private void UpdateSorting()
    {
        if (_mouseDir.y > 0.07f)
            _renderer.sortingOrder = _characterOrder - 1;
        else
            _renderer.sortingOrder = _characterOrder + 1;
    }

    public void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab, _bulletStartTransform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.Setup(_mouseDir);
    }
}
