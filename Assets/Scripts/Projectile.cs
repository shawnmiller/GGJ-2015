﻿using UnityEngine;

public class Projectile : MonoBehaviour
{
	private float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    private Vector2 velocity;
    public Vector2 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

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

    private bool permanent;
    public bool Permanent
    {
        get { return permanent; }
        set { permanent = value; }
    }

	private bool destroyOnCollide;
	public bool DestroyOnCollide
	{
		get { return destroyOnCollide; }
		set { destroyOnCollide = value; }
	}

    private bool canKill;

	void Update()
	{
		transform.position += transform.TransformDirection(velocity) * moveSpeed * Time.deltaTime;

		if (!permanent && !renderer.isVisible)
		{
			gameObject.GetComponent<PooledObject>().ReturnToPool();
		}

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 100f, 1 << LayerMask.NameToLayer("Background")))
        {
            ActiveColorType aCType = hit.collider.GetComponent<ActiveColorType>();
            if (aCType && aCType.type == colorType)
            {
                canKill = false;
            }
            else
            {
                canKill = true;
            }
        }
        else
        {
            canKill = true;
        }
	}

	void OnTriggerEnter(Collider other)
	{
		if (canKill && other.tag == "Player")
		{
			Debug.Log("Hit Player");
			GameManager.Instance.LevelFailed();
		}
		else if (other.tag == "Terrain" && destroyOnCollide)
		{
			Debug.Log("Hit Terrain");
			gameObject.GetComponent<PooledObject>().ReturnToPool();
		}
	}
}
