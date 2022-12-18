using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    private Transform _selfTransform;

    public float MovementSpeed = 50.0f;

    public Action<Projectile> InvisAction;

    private void Start()
    {
        _selfTransform = transform;
    }

    private void OnBecameInvisible()
    {
        //invisibleActions.ForEach(a => a.Invoke(this));
        InvisAction(this);
    }

    private void Update()
    {
        if(gameObject.activeSelf)
        {
            _selfTransform.position += _selfTransform.up * MovementSpeed * Time.deltaTime;
        }
    }
}
