using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Joystick joystick;

    [SerializeField]
    Transform PlayerSprite;

    public Animator animator;

    bool Movement;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gJoystick = GameObject.FindWithTag("MovementJoystick");

        joystick = gJoystick.GetComponent<Joystick>();

        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {

            PlayerSprite.position = new Vector3(joystick.Horizontal + transform.position.x, 0.1f, joystick.Vertical + transform.position.z);

            transform.LookAt(new Vector3(PlayerSprite.position.x, 0, PlayerSprite.position.z));

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            if (joystick.Horizontal > 0 || joystick.Horizontal < 0 || joystick.Vertical > 0 || joystick.Vertical < 0)
            {
                transform.Translate(Vector3.forward * Time.deltaTime);

                if (animator.GetBool("isWalking") != true)
                {
                    animator.SetBool("isWalking", true);
                }
                Movement = true;
            }
            else if (Movement == true)
            {
                animator.SetBool("isWalking", false);

                Movement = false;
            }
        }

    }
}
