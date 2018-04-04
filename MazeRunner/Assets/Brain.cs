using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    int DNALength = 2;

    public GameObject eyes;
    public DNA dna;
    bool seeWall = true;
    bool alive = true;
    public float disFromStart = 0;
    public Vector3 startPos;
    public float maxDis;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dead")
        {
            disFromStart = 0;
            alive = false;
        }
    }

    public void Init()
    {
        //0 forward
        //1 left
        //2 right

        dna = new DNA(DNALength, 360);
        disFromStart = 0;
        alive = true;
    }


	// Update is called once per frame
	void Update () {
		if (!alive)
        {
            return;
        }

        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * .5f, Color.red);
        seeWall = false;
        RaycastHit hit;
        if (Physics.SphereCast(eyes.transform.position, .2f, eyes.transform.forward, out hit, .5f))
        {
            if (hit.collider.gameObject.tag == "wall")
            {
                seeWall = true;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (!alive) return;

        float turn = 0;
        float move = .1f;
        
        if (seeWall)
        {
            
            turn = dna.GetGene(1);
        }


        this.transform.Translate(0, 0, move);
        this.transform.Rotate(0, turn, 0);


        maxDis = Vector3.Distance(startPos, this.transform.position);
        if (maxDis > disFromStart)
        {
            disFromStart = maxDis;
        }
    }
}
