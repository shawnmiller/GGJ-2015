using UnityEngine;
using XInputDotNetPure;

[RequireComponent(typeof(CharacterController))]
public class PlayerRedo : MonoBehaviour
{
    private const float PRETAP = 0.1f;

    private CharacterController controller;
    private KeyWatcher jumpKey;

    public bool controllable;

    public float speed;

    public float gravity;
    public float jumpHeight;
    public float fallAirTime;

    private bool isJumping;
    private bool isGrounded;
    private float airbornTime;

    private float airbornSpeed;

    private Vector3 airTrajectory;
    public float airbornControlAmount;

    public float airDrag;

    private Vector3 lastFlatVelocity;

    private float lastJumpDown;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpKey = new KeyWatcher();
    }

    void Update()
    {
        GamePadState state = GamePad.GetState(PlayerIndex.One);
        jumpKey.Update(state.Buttons.A);
        if(jumpKey.Down()) { lastJumpDown = Time.time; }

        Vector3 input = new Vector3(state.ThumbSticks.Left.X, 0f, 0f);
        Vector3 targetTrajectory = input;
        Vector3 verticalTrajectory = Vector3.zero;

        bool wasGrounded = isGrounded;
        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            isJumping = false;
            airbornTime = 0f;
            if (Time.time - lastJumpDown < PRETAP)
            {
                airbornTime += Time.deltaTime;
                isJumping = true;
                //targetTrajectory = lastFlatVelocity;
                verticalTrajectory.y = jumpHeight;
                verticalTrajectory.y -= gravity * airbornTime * airbornTime;
            }
            else
            {
                verticalTrajectory.y -= gravity;
            }
        }
        else
        {
            if (wasGrounded && !isJumping) { airbornTime = fallAirTime; }
            else { airbornTime += Time.deltaTime; }
            targetTrajectory = lastFlatVelocity * airDrag;
            targetTrajectory += input * airbornControlAmount;
            targetTrajectory = Vector3.ClampMagnitude(targetTrajectory, 1f);

            if (isJumping) { verticalTrajectory.y += jumpHeight; }
            verticalTrajectory.y -= gravity * airbornTime * airbornTime;
        }

        Vector3 combined = (targetTrajectory * speed + verticalTrajectory) * Time.deltaTime;
        controller.Move(combined);

        lastFlatVelocity = targetTrajectory;
    }
}