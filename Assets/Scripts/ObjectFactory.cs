using UnityEngine;
using System.Collections.Generic;

public class ObjectFactory : MSingleton<ObjectFactory>
{

	private Dictionary<string, GameObject> objectsToSpawn = new Dictionary<string, GameObject>();
	public List<GameObject> projectiles = new List<GameObject>();

	void Awake()
	{
		for (int i = 0; i < projectiles.Count; i++)
		{
			objectsToSpawn.Add(projectiles[i].name, projectiles[i].gameObject);
		}
	}

	/*public GameObject Spawn(ProjectileType type, Vector3 pos)
	{
		return Instantiate(GetObject(type), pos, Quaternion.identity) as GameObject;
	}

	public GameObject GetObject(ProjectileType type)
	{
		return objectsToSpawn[type.ToString()];
	}*/
}
