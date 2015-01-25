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
		previousState = currentState;
		currentState = Input.GetKey(key);
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
		return previousState;
	}

	public bool Up()
	{
		return (previousState && !currentState);
	}
}
