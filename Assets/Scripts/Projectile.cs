using UnityEngine;

public class Projectile : MonoBehaviour
{
	//[HideInInspector]
	private float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

	//[HideInInspector]
    private Vector2 velocity;
    public Vector2 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

	//[HideInInspector]
    private ColorType colorType;
    public ColorType ColorType
    {
        get
        { return colorType; }
        set
        {
            colorType = value;
            renderer.material.color = PickColor.Get(colorType);
        }
    }

	//[HideInInspector]
    private bool permanent;
    public bool Permanent
    {
        get { return permanent; }
        set { permanent = value; }
    }

    //void Start()
    //{
    //    renderer.material.color = PickColor.Get(colorType);
    //}

	void Update()
	{
		transform.position += transform.TransformDirection(velocity) * moveSpeed * Time.deltaTime;

		if (!permanent && !renderer.isVisible)
		{
			gameObject.GetComponent<PooledObject>().ReturnToPool();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("Hit Player");
			//GameManager.Instance.LevelFailed();
		}
	}
}
