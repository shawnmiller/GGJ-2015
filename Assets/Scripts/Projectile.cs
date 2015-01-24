using UnityEngine;

public class Projectile : MonoBehaviour
{
	[HideInInspector]
	public float moveSpeed;
	public Vector2 velocity;

	void Update()
	{
		transform.position += transform.TransformDirection(velocity) * moveSpeed * Time.deltaTime;
	}
}
