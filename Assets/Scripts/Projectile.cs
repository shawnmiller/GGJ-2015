using UnityEngine;

public class Projectile : MonoBehaviour
{
	//[HideInInspector]
	public float moveSpeed;

	//[HideInInspector]
	public Vector2 velocity;

	//[HideInInspector]
	public ColorType colorType;

    public bool permanent;

	void Start()
	{
		renderer.material.color = PickColor.Get(colorType);
	}

	void Update()
	{
		transform.position += transform.TransformDirection(velocity) * moveSpeed * Time.deltaTime;

		if (!permanent && !renderer.isVisible)
		{
			gameObject.GetComponent<PooledObject>().ReturnToPool();
		}
	}
}
