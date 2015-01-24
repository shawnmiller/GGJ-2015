using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Texture2D normal;
    public Texture2D blend;

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

	public Vector3 Scaler
	{
		get { return transform.localScale; }
		set { if (value == Vector3.zero) { transform.localScale = Vector3.one; } 
			else { transform.localScale = new Vector3(value.x, value.y, 0.1f); } }
	}

    private PathType pathType;
    public PathType PathType
    {
        get { return pathType; }
        set { pathType = value; }
    }

    private float centerOffset;
    public float CenterOffset
    {
        get { return centerOffset; }
        set { centerOffset = value; }
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
            if (aCType && aCType.Type == colorType)
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

        Color color = renderer.material.color;
        if (canKill)
        {
            renderer.material.mainTexture = normal;
            color.a = 1f;
        }
        else
        {
            renderer.material.mainTexture = blend;
            color.a = 0.3f;
        }
        renderer.material.color = color;
	}

	void OnTriggerStay(Collider other)
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

    public void Apply(ProjectileInfo info)
    {
        this.velocity = info.direction;
        this.moveSpeed = info.speed;
        this.Scaler = info.scale;
        this.ColorType = info.color;
        this.destroyOnCollide = info.destroyOnCollide;
        this.permanent = info.permanent;
        this.pathType = info.path;
    }
}
