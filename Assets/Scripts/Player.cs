using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRB;
    public float bounceForce = 6;
    
    //public AudioSource bounceAudio;
    private AudioManager audioManager;

    private void Start()
    {
     audioManager = FindObjectOfType<AudioManager>();
    }

   private void OnCollisionEnter(Collision collision)
   {
        audioManager.Play("bounce");
        //bounceAudio.Play();
        playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, playerRB.velocity.z);

        //Debug.Log(collision.transform.GetComponent<MeshRenderer>().material.name);
        string materialName = collision.transform.GetComponent<MeshRenderer>().material.name;

        if(materialName == "Safe (Instance)")
        {
          //The ball hit the safe area
        }
        else if(materialName == "Unsafe (Instance)")
        {
          //The ball hits the unsafe area
          GameManager.gameOver = true;
          audioManager.Play("game over");
        }
        else if(materialName == "LastRing (Instance)" && !GameManager.levelCompleted)
        {
          //We completed the level
          GameManager.levelCompleted = true;
          audioManager.Play("win level");
        }
   }
}
