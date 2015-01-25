using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    private Action updateMethod = () => { };

    public GameObject rotator;
    private Transform personalRotator;

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

    private float centerOffset;
    public float CenterOffset
    {
        get { return centerOffset; }
        set { centerOffset = value; }
    }

    private Vector3 centerPoint;

    private bool canKill;

    private Vector3[] tweenPoints;
    private int tweenA;
    private int tweenB;
    private float tweenValue;
    private bool reversed;

    private bool wasVisible;

	void Update()
	{
        updateMethod();
        CheckKillStatus();
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
        this.wasVisible = false;
        this.velocity = info.direction;
        this.moveSpeed = info.speed;
        this.Scaler = info.scale;
        this.ColorType = info.color;
        this.destroyOnCollide = info.destroyOnCollide;
        this.permanent = info.permanent;
        this.reversed = info.reversed;
        DefinePath(info);
    }

    private void MoveLinear()
    {
		if (!permanent)
		{
			transform.position += transform.TransformDirection(velocity) * moveSpeed * Time.deltaTime;

			if (wasVisible && !renderer.isVisible)
			{
				gameObject.GetComponent<PooledObject>().ReturnToPool();
			}
			wasVisible = renderer.isVisible;
		}
    }

    private void MoveCircular()
    {
        Debug.Log(moveSpeed * Time.deltaTime);
        personalRotator.Rotate(0f, 0f, -moveSpeed * Time.deltaTime, Space.World);
        MatchToRotator();
    }

    private void MoveSquarical()
    {
        tweenValue += moveSpeed * Time.deltaTime;
        if (tweenValue > 1f)
        {
            tweenValue -= 1f;
            ShiftTweenPoints();
        }
        transform.position = Vector3.Lerp(tweenPoints[tweenA], tweenPoints[tweenB], tweenValue);
    }

    private void MatchToRotator()
    {
        transform.position = personalRotator.GetChild(0).position;
    }

    private void CheckKillStatus()
    {
		if (GameManager.Instance.LevelCompleted) { canKill = false; }
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

    private void DefinePath(ProjectileInfo info)
    {
        pathType = info.path;
        if (pathType == PathType.Linear)
        {
            updateMethod = MoveLinear;
        }
        else if (pathType == PathType.Circular)
        {
            updateMethod = MoveCircular;
            centerPoint = transform.position;
            personalRotator = ((GameObject)Instantiate(rotator, centerPoint, Quaternion.identity)).transform;
            personalRotator.Rotate(new Vector3(0, 0, 360f * info.startPoint), Space.World);
            personalRotator.GetChild(0).localPosition = Vector3.up * info.centerOffset;
            MatchToRotator();
        }
        else if (pathType == PathType.Squarical)
        {
            updateMethod = MoveSquarical;
            centerPoint = transform.position;
            tweenPoints = new Vector3[4];
            tweenPoints[0] = new Vector3(centerPoint.x - info.centerOffset, centerPoint.y + info.centerOffset, 0);
            tweenPoints[1] = new Vector3(centerPoint.x + info.centerOffset, centerPoint.y + info.centerOffset, 0);
            tweenPoints[2] = new Vector3(centerPoint.x + info.centerOffset, centerPoint.y - info.centerOffset, 0);
            tweenPoints[3] = new Vector3(centerPoint.x - info.centerOffset, centerPoint.y - info.centerOffset, 0);

            if (!info.reversed)
            {
                tweenA = Mathf.FloorToInt(info.startPoint / 0.25f);
                tweenB = tweenA + 1;
                if (tweenB == 4) { tweenB = 0; }
            }
            else
            {
                tweenB = Mathf.FloorToInt(info.startPoint / 0.25f);
                tweenA = tweenB - 1;
                if (tweenB == -1) { tweenB = 3; }
            }
        }
    }

    private void ShiftTweenPoints()
    {
        if (!reversed)
        {
            ++tweenA;
            ++tweenB;

            if (tweenA == 4) { tweenA = 0; }
            if (tweenB == 4) { tweenB = 0; }
        }
        else
        {
            --tweenA;
            --tweenB;

            if (tweenA == -1) { tweenA = 3; }
            if (tweenB == -1) { tweenB = 3; }
        }
    }
}
