using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Joystick joystick;

    [SerializeField]
    Transform PlayerSprite;

    public Animator animator;

    bool Movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        else if (Movement == true) {
            animator.SetBool("isWalking", false);

            Movement = false;
        }

    }
}
