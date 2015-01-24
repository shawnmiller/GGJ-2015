using XInputDotNetPure;

public class KeyWatcher
{
	private ButtonState previousState;
	private ButtonState currentState;

	public KeyWatcher()
	{
		previousState = currentState = ButtonState.Released;
	}

	public void Update(ButtonState state)
	{
		previousState = currentState;
		currentState = state;
	}

	public bool Pressed()
	{
		return currentState == ButtonState.Pressed;
	}

	public bool Down()
	{
		return (previousState == ButtonState.Released && currentState == ButtonState.Pressed);
	}

	public bool Released()
	{
		return currentState == ButtonState.Released;
	}

	public bool Up()
	{
		return (previousState == ButtonState.Pressed && currentState == ButtonState.Released);
	}
}
