using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool _useJoystick;

    private Vector3 dir;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float speed;
    
    [SerializeField]
    Joystick joystick;

    [SerializeField]
    Transform PlayerSprite;

    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject gJoystick = GameObject.FindWithTag("MovementJoystick");

        joystick = gJoystick.GetComponent<Joystick>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 keyDir = new Vector3(Input.GetAxis("Horizontal"), 0.1f, Input.GetAxis("Vertical"));
        Vector3 joystickDir = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        
        dir = keyDir + joystickDir;
        
        if (dir.magnitude < 0.1f)
            return;

        PlayerSprite.position = new Vector3(dir.x + transform.position.x, dir.y, dir.z + transform.position.z);

        transform.LookAt(new Vector3(PlayerSprite.position.x, 0, PlayerSprite.position.z));

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        animator.SetBool("isWalking", dir.magnitude > 0.1f);
        
        //_rb.MovePosition(PlayerSprite.position * Time.deltaTime);

        int i = dir.magnitude > 0.1f ? 1 : 0;
        
        _rb.velocity = transform.forward * (Time.deltaTime * speed * 100 * i);
    }
    
}
