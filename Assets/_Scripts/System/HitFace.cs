﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitFace : MonoBehaviour {

    string[] obstacleTags = { "Obstacle", "Dashable", "Monster"};

      private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Obstacle: Hit something");

        if (collision.gameObject.tag.Equals("Dashable") && Player.isDashing)
        {
            //Do nothing
        }
        else if (collision.gameObject.tag.Equals("Monster") && GameMaster.lifePoint > 0)
        {
            transform.parent.gameObject.GetComponent<Player>().Slowdown();
            GameMaster.removeLife(1);
        }
        else if (Array.IndexOf(obstacleTags, collision.gameObject.tag) > -1 && GameMaster.lifePoint > 0)
        {

            //Bounce();
            //GameMaster.removeLife(1);
            //Invoke("Bounce", 0.5f);

            //StartCoroutine(BounceRoutine());
            Bounce();
            GameMaster.removeLife(1);
            StartCoroutine(Stop());

        }
        else if (Array.IndexOf(obstacleTags, collision.gameObject.tag) > -1 && GameMaster.lifePoint <= 0)
        {
            DeadScene();
        }
    }

    private void Bounce()
    { 
        transform.parent.gameObject.GetComponent<Player>().MoveSpeed = -transform.parent.gameObject.GetComponent<Player>().MoveSpeed;
    }

 

    private void DeadScene()
    {
        SceneManager.LoadScene(2);
    }

   

    //not yet implemented
    //For when crashing, it will pause for a seconds then move forward.
    IEnumerator Stop()
    {
        float forward = -transform.parent.gameObject.GetComponent<Player>().MoveSpeed;
        yield return new WaitForSeconds(0.75f);
        transform.parent.gameObject.GetComponent<Player>().MoveSpeed = 0;
        yield return new WaitForSeconds(2);
        transform.parent.gameObject.GetComponent<Player>().MoveSpeed = forward;
    }
}
