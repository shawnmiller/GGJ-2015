using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static System.Random gen = new System.Random();

	public GameObject projectile;

	public float spawnTime;
	private float currentTime;

    public ProjectileInfo info;
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
			SpawnProjectile();
		}
	}

	void Update()
	{
		if (!info.permanent)
		{
			if (spawnTime == 0f) { return; }
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
        ProjectileInfo temp = info;

        if (info.color == ColorType.RandomAll)
        {
            temp = info.Clone();
            temp.color = (ColorType)gen.Next(0, 6);
        }
        else if (info.color == ColorType.RandomAvoidable)
        {
            temp = info.Clone();
            temp.color = (ColorType)gen.Next(0, 5);
        }

		GameObject obj = ProjectileManager.Instance.Spawn(transform.position);
		Projectile proj = obj.GetComponent<Projectile>();
        proj.Apply(temp);
	}
}
