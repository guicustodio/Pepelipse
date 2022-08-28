using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected Rigidbody rb;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;

        other.TryGetComponent(out damageable);

        damageable?.OnReceiveDamage(damage);
    }
}
