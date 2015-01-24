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
	public bool destroyOnCollide = true;
	public bool preLoad = true;

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

		if (permanant)
		{
			spawnTime = 0f;
			projectileDirection = Vector2.zero;
			projectileSpeed = 0f;

			SpawnProjectile();
		}
	}

	void Update()
	{
		if (!permanant)
		{
			if (preLoad)
			{
				SpawnProjectile();
				preLoad = false;
			}
			else
			{
				currentTime += Time.deltaTime;
				if (currentTime >= spawnTime)
				{
					currentTime -= spawnTime;
					SpawnProjectile();
				}
			}
		}
	}

	void SpawnProjectile()
	{
		GameObject obj = ProjectileManager.Instance.Spawn(transform.position);
		Projectile proj = obj.GetComponent<Projectile>();
		proj.Velocity = projectileDirection;
		proj.MoveSpeed = projectileSpeed;
		proj.ColorType = projectileColor;
		proj.Permanent = permanant;
		proj.DestroyOnCollide = destroyOnCollide;
	}
}
