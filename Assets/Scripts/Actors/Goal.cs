using UnityEngine;

public class Goal : MonoBehaviour
{

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<Player>().canMove = false;
			GameManager.Instance.LevelComplete();
		}
	}

}
