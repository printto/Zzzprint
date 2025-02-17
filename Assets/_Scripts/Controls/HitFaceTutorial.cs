﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitFaceTutorial : MonoBehaviour {

    public Animator animator;

    string[] obstacleTags = { "Obstacle", "Dashable", "Monster"};

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player hit " + other.gameObject.name);
        if (Array.IndexOf(obstacleTags, other.gameObject.tag) > -1 && GameMaster.lifePoint > 0)
        {
            Bounce();
            GameMaster.removeLife(1);
            StartCoroutine(Stop());
        }
        else if (Array.IndexOf(obstacleTags, other.gameObject.tag) > -1 && GameMaster.lifePoint <= 0)
        {
            Bounce();
            DeadScene();
        }
    }

    private void Bounce()
    { 
        transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed = -transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed;
    }

 

    private void DeadScene()
    {
        //SceneManager.LoadScene(2);
        //Initiate.Fade("DeadScene", Color.black, 6f);
        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("DeadScene");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
    }



    //not yet implemented
    //For when crashing, it will pause for a seconds then move forward.
    IEnumerator Stop()
    {
        float forward = -transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed;
        yield return new WaitForSeconds(0.75f);
        transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed = 0;
        yield return new WaitForSeconds(2);
        transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed = forward;
    }
}
