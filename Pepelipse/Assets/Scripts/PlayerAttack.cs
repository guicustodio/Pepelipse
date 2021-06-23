using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    PhotonView view;

    RaycastHit hit;

    public Animator animator;

    bool Shoot;

    [SerializeField]
    Transform PlayerSpine;

    void Start()
    {
        GameObject gJoystick = GameObject.FindWithTag("AttackJoystick");
        GameObject closestEnemy = GameObject.Find("Player");

        view = GetComponent<PhotonView>();
        AttackJoystick = gJoystick.GetComponent<Joystick>();

        FindClosest enemy = closestEnemy.GetComponent<FindClosest>();

    }

    [PunRPC]
    void Update()
    {
        if (view.IsMine)
        {

            if (Mathf.Abs(AttackJoystick.Horizontal) > 0 || Mathf.Abs(AttackJoystick.Vertical) > 0)
            {

                transform.position = new Vector3(Player.position.x, 0, Player.position.z);

                AttackLookAtPoint.position = new Vector3(AttackJoystick.Horizontal + Player.position.x, 0, AttackJoystick.Vertical + Player.position.z);

                transform.LookAt(new Vector3(AttackLookAtPoint.position.x, 0, AttackLookAtPoint.position.z));

                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

                if (Shoot == false)
                {
                    Shoot = true;
                }
            }

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
                    LR.SetPosition(1, new Vector3(LR.GetPosition(1).x, LR.GetPosition(1).y, LR.GetPosition(1).z));
                }

                if (Shoot == false)
                {
                    Shoot = true;
                }
            }
            else if (Shoot && Input.GetMouseButtonUp(0))
            {

                //StartCoroutine(ShootBulletRPC());
                view.RPC("ShootBulletRPC", RpcTarget.All);


                Shoot = false;
            }
            else
            {
                LR.gameObject.SetActive(false);
            }
        }

    }

    [PunRPC]
    IEnumerator ShootBulletRPC() {

        PlayerSpine.LookAt(AttackLookAtPoint);

        PlayerSpine.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        animator.SetBool("isShooting", true);
        //Instantiate(Bullet, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Instantiate(Bullet, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
            animator.SetBool("isShooting", false);
        }
    }

}
