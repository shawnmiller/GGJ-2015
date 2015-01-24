using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public GameObject projectile;

	public float spawnTime;
	private float currentTime;

	public Vector2 projectileDirection;
	public float projectileSpeed;
	public ColorType projectileColor;

	void Start()
	{
		bool error = false;
		if (projectile == null)
		{
			error = true;
			Debug.LogError("Spawner with a missing spawn object: " + name);
		}

		if (error)
		{
			Debug.Break();
		}
	}

	void Update()
	{
		currentTime += Time.deltaTime;
		if (currentTime >= spawnTime)
		{
			currentTime -= spawnTime;
			GameObject obj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
			Projectile proj = obj.GetComponent<Projectile>();
			proj.velocity = projectileDirection;
			proj.moveSpeed = projectileSpeed;
			proj.colorType = projectileColor;
		}
	}
}
