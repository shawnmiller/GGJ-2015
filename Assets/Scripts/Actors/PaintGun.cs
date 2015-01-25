using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PaintGun : MonoBehaviour
{

	public Texture2D reticle;
	public GameObject paintObject;
	public float moveSpeed = 10f;

	private Vector3 stickPos;
	private KeyWatcher xButton;

	void Start()
	{
		stickPos = new Vector3(Screen.width - 0.5f / 2, Screen.height - 0.5f / 2, 0f);
		xButton = new KeyWatcher();
	}

	void Update()
	{
		GamePadState state = GamePad.GetState(PlayerIndex.One);
		xButton.Update(state.Buttons.X);

		if (state.IsConnected)
		{
			Vector3 delta = new Vector3(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, 0f) * moveSpeed * Time.deltaTime;
			
			stickPos += new Vector3(state.ThumbSticks.Left.X, -state.ThumbSticks.Left.Y, 0f) * moveSpeed * Time.deltaTime;

			stickPos = stickPos.Clamp(Vector3.zero, new Vector3(Screen.width * 2 - reticle.width, Screen.height * 2 - reticle.height, 0));

			Vector3 screenCoords = new Vector3(Screen.width * 2 - stickPos.x, Screen.height * 2 - stickPos.y, 0f);

			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenCoords);

			Debug.Log("World point " + worldPoint);

			if (xButton.Pressed())
			{
				RaycastHit hitInfo;
				if (Physics.Raycast(worldPoint, Vector3.forward, out hitInfo, 100f))
				{
					Debug.Log("Hit " + hitInfo.point);
					GameObject instance = Instantiate(paintObject, new Vector3(Mathf.Floor(hitInfo.point.x) * 0.5f, Mathf.Floor(hitInfo.point.y) * 0.5f, 0f), Quaternion.identity) as GameObject;
				}
			}
		}
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect((stickPos.x - reticle.width) / 2, (stickPos.y - reticle.height) / 2, reticle.width, reticle.height), reticle);
	}

}
