using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject projectile;

	public float spawnTime;
	private float currentTime;

	public Vector2 projectileDirection;
	public float projectileSpeed;
	public ColorType projectileColor;
	public bool permanant;

	private static ObjectPool pool;

	void Start()
	{
		bool error = false;
		if (projectile == null)
		{
			error = true;
			Debug.LogError("Spawner with a missing spawn object: " + name);
		}

		if (pool == null)
		{
			pool = new ObjectPool(projectile, 10, true);
		}

		if (error)
		{
			Debug.Break();
		}

		if (permanant)
		{
			spawnTime = -1f;
			projectileDirection = Vector2.zero;
			projectileSpeed = 0f;

			GameObject obj = pool.Get(transform.position);
			Projectile proj = obj.GetComponent<Projectile>();
			proj.velocity = projectileDirection;
			proj.moveSpeed = projectileSpeed;
			proj.colorType = projectileColor;
			proj.permanent = permanant;
		}
	}

	void Update()
	{
		if (!permanant)
		{
			currentTime += Time.deltaTime;
			if (currentTime >= spawnTime)
			{
				currentTime -= spawnTime;
				GameObject obj = pool.Get(transform.position);
				Projectile proj = obj.GetComponent<Projectile>();
				proj.velocity = projectileDirection;
				proj.moveSpeed = projectileSpeed;
				proj.colorType = projectileColor;
				proj.permanent = permanant;
			}
		}
	}
}
