using UnityEngine;

public class Goal : MonoBehaviour
{

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			GameManager.Instance.LevelComplete();
		}
	}

}
