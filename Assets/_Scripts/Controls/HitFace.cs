﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitFace : MonoBehaviour {

    string[] obstacleTags = { "Obstacle"};

    public Animator animator;
    public Animator WhiteController;
    PlayerNew playerScript;

    float LockedMoveSpeed = 0;

    private void Start()
    {
        SceneTransition.setAnimator(animator);
        playerScript = transform.parent.gameObject.GetComponent<PlayerNew>();
        LockedMoveSpeed = playerScript.MoveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Player hit " + other.gameObject.name);
        if (Array.IndexOf(obstacleTags, other.gameObject.tag) > -1 && GameMaster.lifePoint > 0)
        {
            //Bounce();
            GoBack();
            //StartCoroutine(Stop());
        }
        else if (Array.IndexOf(obstacleTags, other.gameObject.tag) > -1 && GameMaster.lifePoint <= 0)
        {
            playerScript.MoveSpeed = 0;
            DeadScene();
        }
        else if (other.gameObject.tag.Equals("EndingGate"))
        {
            playerScript.CanSwipe = false;
            StartCoroutine(WhiteEnd());
        }

    }

   

    private void FixedUpdate()
    {
        if (transform.position.y <= -5 && GameMaster.lifePoint > 0)
        {
            transform.parent.gameObject.transform.position = new Vector3(transform.parent.gameObject.transform.position.x, transform.parent.gameObject.transform.position.y + 0.5f, transform.parent.gameObject.transform.position.z);
            transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            GoBack();
        }
        else if (transform.position.y <= -5 && GameMaster.lifePoint <= 0)
        {
            DeadScene();
        }
    }

    public void GoBack()
    {
        playerScript.CanSwipe = false;
        StartCoroutine(respawn());
        StartCoroutine(blinking());
        GameMaster.removeLife(1);
    }

    private void Bounce()
    {
        playerScript.MoveSpeed = -transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed;
    }

    IEnumerator respawn()
    {
        playerScript.MoveSpeed = 0;
        animator.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        animator.Play("start");
        yield return new WaitForSeconds(2f);
        animator.Play("idle");

    }

    IEnumerator WhiteEnd()
    {
        WhiteController.SetTrigger("WHITE");
        yield return new WaitForSeconds(2f);
        EndingScene();
    }

    private void DeadScene()
    {
        SceneTransition.setScene("DeadScene");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
    }

    private void EndingScene()
    {
        SceneTransition.setScene("EndingScene");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
    }

    IEnumerator blinking()
    {
        GameObject skullo = GameObject.Find("Skullo Material Color");
        skullo.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(true);
        PlayerNew.checkPoint.respawnPlayerAtCheckPoint();
        transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(true);
        transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed = LockedMoveSpeed;
        playerScript.CanSwipe = true;
    }
}
