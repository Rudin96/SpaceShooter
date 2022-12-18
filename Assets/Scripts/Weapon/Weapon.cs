using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _timeBetweenShots = .33f;
    
    private Transform _projSpawn;
    private bool _wantsToShoot = false;

    private bool _canShoot = true;

    private float _timeSinceLastShot = 0;

    private ObjectPool<Projectile> _projectiles;

    //TODO implement object pooling here!


    private void Start()
    {
        _projSpawn = transform;
        _projectiles = new ObjectPool<Projectile>(() => 
            Instantiate(_projectilePrefab, _projSpawn.position, _projSpawn.parent.rotation),
            (proj) => proj.gameObject.SetActive(true), 
            (proj) => proj.gameObject.SetActive(false), 
            (proj) => Destroy(proj.gameObject),
            false, 20, 200);
    }

    public void HandleShoot(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed)
            _wantsToShoot = true;
        else if(callbackContext.canceled)
            _wantsToShoot = false;
    }

    private void Shoot()
    {
        if(_projectilePrefab != null)
        {
            var proj = _projectiles.Get();
            proj.InvisAction = Proj_BecameInvisible;
            proj.transform.position = _projSpawn.position;
            proj.transform.rotation = _projSpawn.parent.rotation;
        }
    }

    private void Proj_BecameInvisible(Projectile proj)
    {
        _projectiles.Release(proj);
    }

    private void CalculateShootingLogic()
    {
        if(_wantsToShoot && _canShoot)
        {
            Shoot();
            _timeSinceLastShot = 0.0f;
            _canShoot= false;
        } else if(_timeSinceLastShot <= _timeBetweenShots)
        {
            _timeSinceLastShot += Time.deltaTime;
        } else
        {
            _canShoot = true;
        }
    }

    private void Update()
    {
        CalculateShootingLogic();
    }
}
