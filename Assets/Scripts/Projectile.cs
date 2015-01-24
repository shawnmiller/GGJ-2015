using UnityEngine;

public class Projectile : MonoBehaviour
{
	//[HideInInspector]
	public float moveSpeed;

	//[HideInInspector]
	public Vector2 velocity;

	//[HideInInspector]
	public ColorType colorType;

    [SerializeField]
    private float test;

	void Start()
	{
		renderer.material.color = PickColor.Get(colorType);
	}

	void Update()
	{
		transform.position += transform.TransformDirection(velocity) * moveSpeed * Time.deltaTime;

		if (!renderer.isVisible)
		{
			gameObject.GetComponent<PooledObject>().ReturnToPool();
		}
	}
}
