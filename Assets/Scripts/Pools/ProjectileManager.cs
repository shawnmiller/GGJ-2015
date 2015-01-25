using UnityEngine;
using System.Collections.Generic;

public class ProjectileManager : MSingleton<ProjectileManager>
{
	public GameObject prefab;
	private static ObjectPool pool;

	private List<GameObject> pooledObjects = new List<GameObject>();

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (pool == null)
		{
			pool = new ObjectPool(prefab, 10, true);
		}
	}

	public GameObject Spawn(Vector3 pos)
	{
		GameObject temp = pool.Get(pos);

		pooledObjects.AddUnique(temp);

		return temp;
	}

	void OnLevelWasLoaded(int level)
	{
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			if (pooledObjects[i].activeInHierarchy)
			{
				pooledObjects[i].GetComponent<PooledObject>().ReturnToPool();
			}
		}

		pooledObjects.Clear();
	}
}
