﻿using UnityEngine;
using UnityEngine.SceneManagement;


public class Monster : MonoBehaviour {

    private float direction;
    bool isHit = false;

	// Use this for initialization
	void Start () {
        var rand = Random.Range(0,2);
        Debug.Log("MonRan : " + rand);
		switch(rand)
        {
            case 0:
                direction = -1f;
                break;
            case 1:
                direction = 1f;
                break;
            default:
                direction = 1f;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, 6 * direction * Time.deltaTime);
	}

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag.Equals("Bullet"))
        {
            Dead();
        }
        else if ((collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("PlayerFace")) && GameMaster.lifePoint > 0)
        {
            if (!isHit)
            {
                isHit = true;
                if (!Player.isTeleporting)
                {
                    GameMaster.removeLife(1);
                }
            }
            Dead();
        }
        else if ((collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("PlayerFace")) && GameMaster.lifePoint <= 0)
        {
            if(!Player.isTeleporting)
            {
                EndGame();
            }
            Dead();
        }
        else if (collision.collider.tag.Equals("LWall") || collision.collider.tag.Equals("RWall"))
        {
            Debug.Log("Monster hit wall!!");
            ChangeDirection();
        }
        
        /*
        if (!collision.gameObject.tag.Equals("Player") && !collision.gameObject.tag.Equals("PlayerFace"))
        {
            ChangeDirection();
        }
        */
    }

    void EndGame()
    {
        //SceneManager.LoadScene(2);
        Initiate.Fade("DeadScene", Color.black, 4f);

    }

    void ChangeDirection()
    {
        direction *= -1f;
    }

    void Dead()
    {
        Destroy(gameObject);
    }
}
