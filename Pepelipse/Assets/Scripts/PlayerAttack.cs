using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] LineRenderer _lineRenderer;

    [SerializeField] private Joystick _joystick;
    
    private AttackJoystick _attackJoystick;

    [SerializeField] private FindClosest _findClosest;
    
    private RaycastHit hit;
    
    [SerializeField] Transform AttackLookAtPoint;
    
    [SerializeField] Transform Player;
    [SerializeField] private Transform shotPos;
    
    public Animator animator;

    [SerializeField] private GameObject _AutoShotPrefab;
    [SerializeField] private GameObject _AimShotPrefab;

    [SerializeField] public float _maxTrailDistance;

    [SerializeField] private float timeToAimedShot;

    private float aimTimer;


    private IEnumerator TimeAimedCalc;

    void Start()
    {
        GameObject gJoystick = GameObject.FindWithTag("AttackJoystick");
        _joystick = gJoystick.GetComponent<Joystick>();
        _attackJoystick = FindObjectOfType<AttackJoystick>();

        TimeAimedCalc = AimTime();
        
        _attackJoystick.PointerDownEvent += OnPressAttackButton;
        _attackJoystick.PointerUpEvent += OnReleaseAttackButton;

    }

    private void OnDestroy()
    {
        _attackJoystick.PointerDownEvent -= OnPressAttackButton;
        _attackJoystick.PointerUpEvent -= OnReleaseAttackButton;
    }

    void Update()
    { 
        HandleLook();
        HandleLineRenderer();
    }

    void HandleLook()
    {
        if (aimTimer > 0)
        {
            transform.position = new Vector3(Player.position.x, 0, Player.position.z);

            AttackLookAtPoint.position = new Vector3(_joystick.Horizontal + Player.position.x, 0, _joystick.Vertical + Player.position.z);

            transform.LookAt(new Vector3(AttackLookAtPoint.position.x, 0, AttackLookAtPoint.position.z));

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
    
    void HandleLineRenderer()
    {
        float hitDistance = 0;
            
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxTrailDistance))
        {
            hitDistance = hit.distance;
        }

        float trailDistance = hitDistance < 0.1f ? _maxTrailDistance : hitDistance;
            
        _lineRenderer.SetPosition(1, new Vector3(0, 0.1f, trailDistance));
    }

    void OnPressAttackButton()
    {
        StartCoroutine(TimeAimedCalc);
        _lineRenderer.gameObject.SetActive(true);
    }
    
    void OnReleaseAttackButton()
    {
        _lineRenderer.gameObject.SetActive(false);
        
        //Calcular quanto tempo foi pressionado e disparar de acordo
        HandleShooting();
    }

    void HandleShooting()
    {
        if (IsAimedShot())
        {
            AimedShot();
        }
        else
        {
            AutoShot();
        }

        aimTimer = 0;
        StopCoroutine(TimeAimedCalc);
    }

    void AimedShot()
    {
        //Instanciar bala
        Instantiate(_AimShotPrefab, shotPos.position, Quaternion.Euler(transform.localEulerAngles));
        Debug.Log("AimedShot");
    }

    void AutoShot()
    {
        Transform target = _findClosest.bestTarget;
        
        if (target)
        {
            transform.LookAt(new Vector3(target.position.x, 0, target.position.z));
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            
            Instantiate(_AutoShotPrefab, shotPos.position, Quaternion.Euler(transform.localEulerAngles));
            Debug.Log("AutoShot");
        }
    }
    
    private IEnumerator AimTime()
    {
        aimTimer = 0;
        
        while (!IsAimedShot())
        {
            aimTimer += Time.deltaTime;
            yield return null;
        }
    }

    bool IsAimedShot()
    {
        return aimTimer >= timeToAimedShot;
    }
    

    
}
