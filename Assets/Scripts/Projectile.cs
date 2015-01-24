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
    public bool collidable;

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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Start check");
        Debug.Log(other.name);
        Debug.Log(transform.position);
        if(other.tag == "Player"){
            GameManager.Instance.LevelFailed();
        }
        else if(collidable && other.tag == "Terrain" && !permanent){
            Debug.Log("Success");
            gameObject.GetComponent<PooledObject>().ReturnToPool();
        }

    }
}
