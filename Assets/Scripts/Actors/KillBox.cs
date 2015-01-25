using UnityEngine;

public class KillBox : MonoBehaviour
{

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			GameManager.Instance.LevelFailed();
		}
	}

}
