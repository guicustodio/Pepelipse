using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{

    public GameObject playerPrefab;
    Joystick moveJoystick;
    Joystick AttackJoystick;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    void Start()
    {
        //moveJoystick.GetComponent<PlayerMovement>();
        //AttackJoystick.GetComponent<PlayerAttack>();

        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }


}
