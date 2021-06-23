using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		FindClosestEnemy ();
	}

	void FindClosestEnemy()
	{
		float distanceToClosestEnemy = Mathf.Infinity;
		Enemy closestEnemy = null;
		Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

		foreach (Enemy currentEnemy in allEnemies) {
			float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
			if (distanceToEnemy < distanceToClosestEnemy) {
				distanceToClosestEnemy = distanceToEnemy;
				closestEnemy = currentEnemy;
			}
		}

		Debug.DrawLine (this.transform.position, closestEnemy.transform.position);
	}

}
