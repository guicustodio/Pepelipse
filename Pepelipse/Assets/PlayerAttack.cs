using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField]
    LineRenderer LR;

    [SerializeField]
    Joystick AttackJoystick;

    [SerializeField]
    Transform AttackLookAtPoint;

    [SerializeField]
    public float TrailDistance;

    [SerializeField]
    Transform Player;

    [SerializeField]
    Transform Bullet;

    RaycastHit hit;

    bool Shoot;

    void Start()
    {
        
    }

    void Update()
    {
        if (Mathf.Abs(AttackJoystick.Horizontal) > 0.5f || Mathf.Abs(AttackJoystick.Vertical) > 0.5f)
        {
            if (LR.gameObject.activeInHierarchy == false)
            {
                LR.gameObject.SetActive(true);
            }
            transform.position = new Vector3(Player.position.x, 0, Player.position.z);

            AttackLookAtPoint.position = new Vector3(AttackJoystick.Horizontal + Player.position.x, 0, AttackJoystick.Vertical + Player.position.z);

            transform.LookAt(new Vector3(AttackLookAtPoint.position.x, 0, AttackLookAtPoint.position.z));

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            LR.SetPosition(0, transform.position);

            if (Physics.Raycast(transform.position, transform.forward, out hit, TrailDistance))
            {
                LR.SetPosition(1, hit.point);
            }
            else
            {
                LR.SetPosition(1, transform.forward * TrailDistance);
                LR.SetPosition(1, new Vector3(LR.GetPosition(1).x, 0, LR.GetPosition(1).z));
            }

            if (Shoot == false)
            {
                Shoot = true;
            }
        }
        else if (Shoot && Input.GetMouseButtonUp(0)) {
            Debug.Log("asd");

            Instantiate(Bullet, transform.position, transform.rotation);

            Shoot = false;
        }
        else
        {
            LR.gameObject.SetActive(false);
        }
    }
}
