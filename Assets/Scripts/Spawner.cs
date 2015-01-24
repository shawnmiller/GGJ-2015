using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public ProjectileType type;

	public float spawnTime;
	private float currentTime;

	public float projectileSpeed;

	void Start()
	{
		bool error = false;
		if (type == ProjectileType.None)
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
			GameObject obj = ObjectFactory.Instance.Spawn(type, transform.position);
			Projectile projectile = obj.GetComponent<Projectile>();
			projectile.moveSpeed = projectileSpeed;
		}
	}
}
