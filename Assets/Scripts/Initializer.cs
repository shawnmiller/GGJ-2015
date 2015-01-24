using UnityEngine;

public class Initializer : MonoBehaviour
{

	public GameObject gameManager;
	public GameObject projectileManager;

	void Awake()
	{
		if (!GameObject.FindObjectOfType<GameManager>())
		{
			Instantiate(gameManager);
		}

		if (!GameObject.FindObjectOfType<ProjectileManager>())
		{
			Instantiate(projectileManager);
		}

		Destroy(gameObject);
	}
}
