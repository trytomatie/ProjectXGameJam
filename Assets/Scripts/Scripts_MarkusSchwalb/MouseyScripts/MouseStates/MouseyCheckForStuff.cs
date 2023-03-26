using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Markus Schwalb
/// Varios checks if something is in sight usw.
/// </summary>
public class MouseyCheckForStuff : MouseBaseState
{
    private float distance;

    public override void EnterMouseState(MouseStateManager Mouse)
    {

    }

    /// <summary>
    /// Checkdistance between Mousey and Player
    /// Check if Mousey sees the player
    /// Check if the player is to loud
    /// </summary>
    /// <param name="Mouse"></param>
    public override void UpdateMouseState(MouseStateManager Mouse)
    {

        calculateDistance(Mouse);

        //Debug.Log("CheckForStuff");
        Vector3 rayCastOrigin = new Vector3(Mouse.transform.position.x, Mouse.transform.position.y + Mouse.eyeHeight, Mouse.transform.position.z);
        if (CheckPlayerInView(Mouse.player, Mouse))
        {
            if (Mouse.currentState != Mouse.mouseyBlended)
            {
                Mouse.SwitchMouseState(Mouse.mChase);
            }
        }

        CheckForNoise(Mouse);
    }


    public override void ExitMouseState(MouseStateManager Mouse)
    {

    }

    /// <summary>
    /// Check if the player is in Mouseys fieldOf View if so check if he gets hit by the raycast (player hit by raycast)
    /// </summary>
    /// <param name="player"></param>
    /// <param name="Mouse"></param>
    /// <returns></returns>
    private bool CheckPlayerInView(GameObject player, MouseStateManager Mouse)
    {
        
        //Debug.Log("CheckPlayerInView");
        //create a Vector 3 as an direction vector between Mousey and Player
        Vector3 vectorOrigin = new Vector3(Mouse.transform.position.x, Mouse.transform.position.y + Mouse.eyeHeight, Mouse.transform.position.z);
        Vector3 vectorBetween = player.transform.position - vectorOrigin;
        //calculate the angle
        float angle = Vector3.Angle(vectorBetween, vectorOrigin);
        //Debug.DrawRay(vectorOrigin, (vectorBetween.normalized * Mouse.mouseyViewingDistance), Color.yellow);
        //Check if the player is in Mouseys FieldOfView
        if (Mouse.mouseyFieldOfView > angle)
        {
            //Debug.Log("CheckPlayerInViewTrue");

            //Check if player can be seen
            if (playerHitByRaycast(Mouse, vectorBetween, vectorOrigin))
            {
                return true;
            }

        }
        //Debug.Log("CheckPlayerInViewFalse");
        return false;
    }

    /// <summary>
    /// Check if player is hit by raycast
    /// </summary>
    /// <param name="Mouse"></param>
    /// <param name="vectorToPlayer"></param>
    /// <returns></returns>
    private bool playerHitByRaycast(MouseStateManager Mouse, Vector3 vectorToPlayer, Vector3 vectorOrigin)
    {
        //Debug.Log("playerHitbyRaycasttest");

        //calculate Raycast origin 
        //Vector3 rayCastOrigin = new Vector3(Mouse.transform.position.x, Mouse.transform.position.y + Mouse.eyeHeight, Mouse.transform.position.z);
        
        RaycastHit hit;

        
        if(Physics.Raycast(vectorOrigin, vectorToPlayer, out hit, Mouse.mouseyViewingDistance, Mouse.mouseRayCastLayers))
        {
            if (hit.collider != null)
            {
                //Debug.Log("HitSomething");
                GameObject hitObject = hit.transform.gameObject;

                //check if the hit is the player
                if (hitObject.CompareTag("Player"))
                {
                    //Debug.Log("Hitplayer");
                    Debug.DrawRay(vectorOrigin, (vectorToPlayer.normalized * hit.distance), Color.green);
                    return true;
                }
                
            }
        }
        Debug.DrawRay(vectorOrigin, (vectorToPlayer.normalized * Mouse.mouseyViewingDistance), Color.red);

        return false;
        
    }

    
    /// <summary>
    /// Check if collided thing is a cheese if so the Mouse gets Distracted and chases after the Cheese a Mouse can smell cheese therefore it doesnt have to see it ;D
    /// </summary>
    /// <param name="other"></param>
    /// <param name="Mouse"></param>
   public void MouseTrigger(Collider other, MouseStateManager Mouse)
    {
        /*if (Mouse.currentState != Mouse.mChase)
        {
            if (other.gameObject.GetComponent<Interactable_Item>() != null)
            {
                GameObject item = other.gameObject;
                Interactable_Item itemScript = item.GetComponent<Interactable_Item>();
                if (itemScript.itemType == Interactable_Item.ItemType.Cheese && Mouse.cheese==null)
                {
                    Mouse.cheese = item;
                    Mouse.SwitchMouseState(Mouse.mouseCheese);
                }
            }
        }*/
        
    }

    /// <summary>
    /// get distance between player and mousey and if the distance is below Catch Distance and mousey is chasing you then you lose
    /// </summary>
    /// <param name="Mouse"></param>
    private void calculateDistance(MouseStateManager Mouse)
    {
        distance = Vector3.Distance(Mouse.player.transform.position, Mouse.transform.position);
        if (Mouse.currentState == Mouse.mChase && distance < Mouse.catchDistance)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            Debug.Log("verloren");
        }
    }

    /// <summary>
    /// Check if the player is close enough to hear him if so Chase him unless you eat cheese
    /// </summary>
    /// <param name="Mouse"></param>
    private void CheckForNoise(MouseStateManager Mouse)
    {
        
        // player = Mouse.player.GetComponent<PlayerController>();
        //if (player.noise > distance /*&& Mouse.currentState != Mouse.mouseCheese*/)
        //{
        //    Mouse.SwitchMouseState(Mouse.mChase);
        //}
    }
}
