using UnityEngine;
using XInputDotNetPure;

[RequireComponent(typeof(CharacterController))]
public class PlayerTwo : MonoBehaviour
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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        aButton = new KeyWatcher();
        velocity = Vector2.zero;
    }

    void Update()
    {
        GamePadState state = GamePad.GetState(PlayerIndex.One);
        aButton.Update(state.Buttons.A);

        onGround = controller.isGrounded;//Physics.Raycast(transform.position, Vector3.down, 1.2f, ~(1 << LayerMask.NameToLayer("Player")));

        if (onGround)
        {
            airbornTime = 0f;
        }
        else
        {
            airbornTime += Time.deltaTime;
        }

        if (state.IsConnected)
        {
            velocity = new Vector2(state.ThumbSticks.Left.X * Time.deltaTime, 0f);

            if (aButton.Down() && onGround)
            {
                isJumping = true;
            }
        }

        if (isJumping)
        {
            jumpTime += Time.deltaTime;

            velocity.y = (jumpStrength * Time.deltaTime) - airbornTime * airbornTime * gravity * Time.deltaTime;

            if (jumpTime > 0.5f && controller.isGrounded)
            {
                isJumping = false;
                jumpTime = 0f;
            }
        }

        if (!onGround && !isJumping)
        {
            velocity.y -= airbornTime * airbornTime * gravity * Time.deltaTime;
        }

        controller.Move(velocity * moveSpeed);// * Time.deltaTime);
    }

}
