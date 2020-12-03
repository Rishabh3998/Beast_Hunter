using UnityEngine;

public class ZombieScript : MonoBehaviour {
  //declare the transform of our goal (where the navmesh agent will move towards) and our navmesh agent (in this case our zombie)
  private Transform goal;
  private UnityEngine.AI.NavMeshAgent agent;

  // Use this for initialization
  void Start () {
  
    //create references
    goal = Camera.main.transform;
    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    //set the navmesh agent's desination equal to the main camera's position (our first person character)
    agent.destination = goal.position;
    //start the walking animation
    GetComponent<Animation>().Play ("Walk");
  }


  //for this to work both need colliders, one must have rigid body, and the zombie must have is trigger checked.
  void OnTriggerEnter (Collider col)
  {
    //first disable the zombie's collider so multiple collisions cannot occur
    GetComponent<CapsuleCollider>().enabled = false;
    //destroy the bullet
    Destroy(col.gameObject);
    //stop the zombie from moving forward by setting its destination to it's current position
    agent.destination = gameObject.transform.position;
    //stop the walking animation and play the falling back animation
    //GetComponent<Animation>().Stop ();
    GetComponent<Animation>().Play ("Death");
    //destroy this zombie in six seconds.
    Destroy (gameObject, 6);
    //instantiate a new zombie
    GameObject Zombie = Instantiate(Resources.Load("Zombie", typeof(GameObject))) as GameObject;

    //set the coordinates for a new vector 3
    float randomX = UnityEngine.Random.Range (-12f,12f);
    float constantY = .01f;
    float randomZ = UnityEngine.Random.Range (-13f,13f);
    //set the zombies position equal to these new coordinates
    Zombie.transform.position = new Vector3 (randomX, constantY, randomZ);

    //if the zombie gets positioned less than or equal to 3 scene units away from the camera we won't be able to shoot it
    //so keep repositioning the zombie until it is greater than 3 scene units away. 
    while (Vector3.Distance (Zombie.transform.position, Camera.main.transform.position) <= 3) {
      
      randomX = UnityEngine.Random.Range (-12f,12f);
      randomZ = UnityEngine.Random.Range (-13f,13f);

      Zombie.transform.position = new Vector3 (randomX, constantY, randomZ);
    }

  }

}