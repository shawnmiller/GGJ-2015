using UnityEngine;
using XInputDotNetPure;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{

    private CharacterController controller;
    private KeyWatcher aButton;

    private Vector2 velocity;
    public float moveSpeed;
    public float gravity;

    private bool isJumping;
    private bool onGround;

    private float jumpTime = 0f;
    public float jumpStrength;

    private float airbornTime;

	public bool canMove;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        aButton = new KeyWatcher();
        velocity = Vector2.zero;
		canMove = true;
    }

    void Update()
    {
		if (canMove)
		{
			GamePadState state = GamePad.GetState(PlayerIndex.One);
			aButton.Update(state.Buttons.A);

			if (state.IsConnected)
			{
				onGround = controller.isGrounded;//Physics.Raycast(transform.position, Vector3.down, 0.6f, ~(1 << LayerMask.NameToLayer("Player")));

				if (onGround)
				{
					airbornTime = 0f;
				}
				else
				{
					airbornTime += Time.deltaTime;
				}

				velocity = new Vector2(state.ThumbSticks.Left.X, 0f) * moveSpeed;

				if (aButton.Down() && onGround)
				{
					isJumping = true;
				}

				if (isJumping)
				{
					jumpTime += Time.deltaTime;

					velocity.y = jumpStrength - airbornTime * airbornTime * gravity;

					if (jumpTime > 0.5f && onGround)
					{
						isJumping = false;
						jumpTime = 0f;
					}
				}

				if (!onGround && !isJumping)
				{
					velocity.y -= airbornTime * airbornTime * gravity;
				}

				if (onGround)
				{
					velocity.y -= gravity * Time.deltaTime;
				}

				controller.Move(velocity * Time.deltaTime);
			}
		}
    }

}
