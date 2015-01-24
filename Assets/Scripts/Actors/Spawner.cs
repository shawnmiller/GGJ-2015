using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject projectile;

	public float spawnTime;
	private float currentTime;

    public ProjectileInfo info;

    //public Vector2 projectileDirection;
    //public float projectileSpeed;
    //public Vector3 projectileScale;
    //public ColorType projectileColor;
    //public PathType projectilePathType;
    //[Tooltip("Center offset only works with non-linear paths.")]
    //public float projectileCenterOffset;
    //[Tooltip("Must be a value between 0 and 1")]
    //public float projectileStartPoint;

    //public bool permanant;
    //public bool destroyOnCollide = true;
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

		if (info.permanent)
		{
			spawnTime = 0f;
            //projectileDirection = Vector2.zero;
            //projectileSpeed = 0f;
            info.direction = Vector2.zero;
            info.speed = 0f;

			SpawnProjectile();
		}
	}

	void Update()
	{
		if (!info.permanent)
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
        proj.Apply(info);
        //proj.Velocity = projectileDirection;
        //proj.MoveSpeed = projectileSpeed;
        //proj.Scaler = projectileScale;
        //proj.ColorType = projectileColor;
        //proj.Permanent = permanant;
        //proj.DestroyOnCollide = destroyOnCollide;
	}
}
