using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisucchioProva : MonoBehaviour
{
    public Animation a;
    public AnimationClip ac;
    public GameObject target;
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        a = this.GetComponent<Animation>();
        //ac=a.GetComponents<Animation>()[0].GetComponent<AnimationClip>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) )
        {
            //ac.Play();
            a.GetComponents<Animation>()[0].Play();
            this.GetComponentInParent<Transform>().position= target.GetComponent<Transform>().position;
            anim=a.GetComponents<Animation>()[0];
            Debug.Log("S  U  C   C "+anim.name+" "+ anim.isPlaying);
            Debug.Log("Coordinates of target:" + target.GetComponent<Transform>().position.ToString());
            Debug.Log("Coordinates of object:" + this.GetComponentInParent<Transform>().position.ToString());
        }
    }
}
