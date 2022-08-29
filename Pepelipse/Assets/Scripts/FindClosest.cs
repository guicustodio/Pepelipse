using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour
{

	public Transform bestTarget;
	
	// Update is called once per frame
	void Update () {
		FindClosestEnemy ();
	}

	void FindClosestEnemy()
	{
		float distanceToClosestEnemy = Mathf.Infinity;
		Enemy closestEnemy = null;
		Enemy[] allEnemies = FindObjectsOfType<Enemy>(); //Trocar por SphereCast

		foreach (Enemy currentEnemy in allEnemies) {
			float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
			if (distanceToEnemy < distanceToClosestEnemy) {
				distanceToClosestEnemy = distanceToEnemy;
				closestEnemy = currentEnemy;
				bestTarget = closestEnemy.transform;
			}
		}

		Debug.DrawLine (this.transform.position, closestEnemy.transform.position);
	}

}
