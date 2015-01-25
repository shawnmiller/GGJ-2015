using UnityEngine;

public class Goal : MonoBehaviour
{

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<PlayerRedo>().controllable = false;
			GameManager.Instance.LevelComplete();
		}
	}

}
