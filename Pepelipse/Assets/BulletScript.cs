using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [SerializeField]
    PlayerAttack PA;

    [SerializeField]
    float speed;

    Vector3 BulletEndDist;

    // Start is called before the first frame update
    void Start()
    {
        PA = GameObject.Find("AttackTrail").GetComponent<PlayerAttack>();
        BulletEndDist = transform.position + transform.forward * PA.TrailDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == BulletEndDist) {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector3.forward * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
        else 
        {
            Destroy(this.gameObject);

        }
    }
}
