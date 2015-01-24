using UnityEngine;

public class PooledObject : MonoBehaviour
{
	private ObjectPool owner;

	public void SetPool(ObjectPool owner)
	{
		this.owner = owner;
	}

	public void ReturnToPool()
	{
		owner.Return(this);
	}
}
