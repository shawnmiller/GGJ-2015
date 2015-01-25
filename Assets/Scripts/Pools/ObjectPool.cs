using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ObjectPool
{
	public GameObject prefab;

	public Stack<GameObject> pool;

	public ObjectPool(GameObject obj, int capacity, bool preload)
	{
		prefab = obj;
		pool = new Stack<GameObject>(capacity);

		if (preload)
		{
			for (int i = 0; i < capacity; ++i)
			{
				CreateNewInstance(Vector2.zero);
			}
		}
	}

	public GameObject Get(Vector3 pos)
	{
		if (pool.Count == 0)
		{
			CreateNewInstance(pos);
		}

		GameObject instance = pool.Pop();
		instance.transform.position = pos;
		instance.SetActive(true);
		return instance;
	}

	public void Return(PooledObject obj)
	{
		obj.gameObject.SetActive(false);
		pool.Push(obj.gameObject);
		Debug.Log("Object returned to pool: " + obj.name);
	}

	public void CreateNewInstance(Vector3 pos)
	{
		GameObject instance = (GameObject)GameObject.Instantiate(prefab);
		MonoBehaviour.DontDestroyOnLoad(instance);
		instance.transform.position = pos;
		PooledObject pObj = instance.GetComponent<PooledObject>();

		if (!pObj)
		{
			pObj = instance.AddComponent<PooledObject>();
			Debug.LogWarning("No PooledObject script found on " + prefab.name);
		}
		pObj.SetPool(this);
		instance.SetActive(false);
		pool.Push(instance);
	}

}
