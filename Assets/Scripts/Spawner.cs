using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject spawn;

    public float spawnTime;
    private float currentTime;

    public float projectileSpeed;

	void Start ()
    {
        bool error = false;
        if (!spawn)
        {
            error = true;
            Debug.LogError("Spawner with a missing spawn object: " + name);
        }
        //if (!spawn.GetComponent<Projectile>())
        //{
        //    error = true;
        //    Debug.LogError("Object to spawn is missing a Projectile script: " + spawn.name);
        //}
        if (error)
        {
            Debug.Break();
        }
	}
	
	void Update ()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= spawnTime)
        {
            currentTime -= spawnTime;
            GameObject obj = Instantiate(spawn, transform.position, Quaternion.identity) as GameObject;
            Projectile projectile = obj.GetComponent<Projectile>();
            projectile.moveSpeed = projectileSpeed;
        }
	}
}
