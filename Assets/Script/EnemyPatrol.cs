using UnityEngine;

public class EnemyPatrol : MonoBehaviour



{
    public float speed ;
    public Transform[] waypoints ;

    private Transform target ;
    private int destPoint = 0;
     public SpriteRenderer graphics ;

    // Start is called before the first frame update

    void Start()
    {
        target=waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Calcul du vecteur de direction entre la position actuelle de l'ennemi et la position de la cible 

        Vector3 dir =target.position - transform.position; 

        // Translation de l'ennemi dans la direction calculée à une vitesse spécifiée.

        transform.Translate(dir.normalized*speed*Time.deltaTime, Space.World) ;
        
        //Si l'ennemi est presque arrivé à sa destination
        if(Vector3.Distance(transform.position, target.position)< 0.3f) 
        {

            destPoint = (destPoint + 1) % waypoints.Length ;
            target = waypoints[destPoint];

            graphics.flipX = !graphics.flipX ;

        }
    }
}
