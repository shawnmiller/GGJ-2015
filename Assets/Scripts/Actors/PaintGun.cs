using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PaintGun : MonoBehaviour
{
    private const float SENSITIVITY = 0.05f;
    private const float MIN_SPACING = 1f;
    private const float OFFSET = 0.001f;

	public Texture2D reticle;
	public GameObject paintObject;
    public float moveSpeed = 10f;

	private Vector3 stickPos;
	private KeyWatcher xButton;

    private Vector3 cursorPosition;

    private GameObject marker;

    private Vector3 lastSpawn;
    private int spawned;

	void Start()
	{
        cursorPosition = new Vector3(0.5f, 0.5f, 0f);
		//stickPos = new Vector3(Screen.width - 0.5f / 2, Screen.height - 0.5f / 2, 0f);
		xButton = new KeyWatcher();
        marker = new GameObject();
	}

	void Update()
	{
		GamePadState state = GamePad.GetState(PlayerIndex.One);
		xButton.Update(state.Buttons.X);

		if (state.IsConnected)
		{
            
            Vector3 delta = new Vector3(state.ThumbSticks.Left.X, -state.ThumbSticks.Left.Y, 0f) * moveSpeed * SENSITIVITY * Time.deltaTime;
            Vector3 newPosition = cursorPosition + delta;
            newPosition = newPosition.Clamp(Vector3.zero, Vector3.one);
            cursorPosition = newPosition;

            Vector3 refPoint = newPosition;
            refPoint.y = 1f - refPoint.y;

            Vector3 worldPoint = Camera.main.ViewportToWorldPoint(refPoint);
            marker.transform.position = worldPoint;

            RaycastHit hit;
            Physics.Raycast(marker.transform.position, Vector3.forward, out hit);

            if (xButton.Pressed())
            {
                if (hit.collider != null && Vector3.Distance(hit.point, lastSpawn) > MIN_SPACING)
                {
                    GameObject instance = Instantiate(paintObject, hit.point, Quaternion.identity) as GameObject;
                    Vector3 newPos = instance.transform.position;
                    newPos.z = -spawned * OFFSET;
                    instance.transform.position = newPos;
                    instance.renderer.material.color = (spawned % 2 == 0 ? Color.blue : Color.red);

                    lastSpawn = instance.transform.position;
                    ++spawned;
                }
            }
		}
	}

	void OnGUI()
	{
        int x = Screen.width;
        int y = Screen.height;
        Rect dRect = new Rect(
            (cursorPosition.x * x) - (reticle.width / 2),
            (cursorPosition.y * y) - (reticle.height / 2),
            reticle.width, reticle.height);
        GUI.DrawTexture(dRect, reticle);
	}

}
