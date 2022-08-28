using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualAimBullet : BulletBase
{
    [SerializeField]
    private int _CollisionsToDestroy;

    private int _Collisions;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        _Collisions++;
        
        if (_Collisions >= _CollisionsToDestroy)
            Destroy(gameObject);
    }
}
