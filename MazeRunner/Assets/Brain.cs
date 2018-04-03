using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    int DNALength = 2;

    public GameObject eyes;
    
    public DNA dna;
    bool seeWall = true;
    bool alive = true;
    public float disFromStart;
    public Vector3 startPos;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dead")
        {
            alive = false;
        }
    }

    public void Init()
    {
        //0 forward
        //1 left
        //2 right

        dna = new DNA(DNALength, 3);
        disFromStart = 0;
        alive = true;
    }


	// Update is called once per frame
	void Update () {
		if (!alive)
        {
            return;
        }

        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 1, Color.red, 10);
        seeWall = false;
        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "wall")
            {
                seeWall = true;
            }
        }

        float turn = 0;
        float move = 0;

        if (seeWall)
        {
            if (dna.GetGene(1) == 0) move = 1;
            else if (dna.GetGene(1) == 1) turn = -90;
            else if (dna.GetGene(1) == 2) turn = 90;
        }

        else
        {
            if (dna.GetGene(0) == 0) move = 1;
            else if (dna.GetGene(0) == 1) turn = -90;
            else if (dna.GetGene(0) == 2) turn = 90;
        }

        this.transform.Translate(0, 0, move * .1f);
        this.transform.Rotate(0, turn, 0);

        disFromStart = Vector3.Distance(startPos, this.transform.position);
    }
}
