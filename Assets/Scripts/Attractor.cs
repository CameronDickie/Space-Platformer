using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {
    /*
     * Cameron Dickie
     * December 21st 2017
     * Creates a script to attract two objects with a rigidbody together
     */
    

    const float G = 6674f;

    public static List<Attractor> Attractors;

    public Rigidbody2D rb;
    public float limit;
    private void FixedUpdate()
    {
        
        foreach (Attractor attractor in Attractors)
        {
            if (attractor != this)
            {
                Attract(attractor); // attracts all objects in the static list
            }
        }
    }

    void OnEnable()
    {
        if (Attractors == null)
        {
            Attractors = new List<Attractor>(); //init array if empty
        }
        Attractors.Add(this); // add this attractor to the static list
    }
    void Attract(Attractor objToAttract)
    {
        if(objToAttract == null)
        {
            return;
        }
        Rigidbody2D rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position; // find the direction to attract 
        float distance = direction.magnitude;

        
        float forceMagnitude = G* (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        
        if(distance > limit)
        {
            forceMagnitude = 0;
        }
        Vector3 force = direction.normalized * forceMagnitude; // creating a vector of the force and direction
 
        rbToAttract.AddForce(force);
        
    }
}
