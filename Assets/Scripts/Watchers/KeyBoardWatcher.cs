using UnityEngine;

public class KeyBoardWatcher
{
	private KeyCode key;
	private bool previousState;
	private bool currentState;

	public KeyBoardWatcher(KeyCode key)
	{
		this.key = key;
	}

	public void Update()
	{
		currentState = Input.GetKey(key);
		previousState = currentState;
	}

	public bool Pressed()
	{
		return currentState;
	}

	public bool Down()
	{
		return (!previousState && currentState);
	}

	public bool Released()
	{
		return !currentState;
	}

	public bool Up()
	{
		return (previousState && !currentState);
	}
}
