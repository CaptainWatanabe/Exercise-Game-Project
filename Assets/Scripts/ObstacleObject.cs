using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MovementForObject
{

    [SerializeField] Transform nextDestination;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // if (InGameInput.inputInstance.runningPower > 1.5f)
          //  {
                switch (moveForObject)
                {
                    case ObjectType.Ground:
                    if (other.GetComponent<PlayerController>().isSliding)
                    {
                        other.GetComponent<PlayerController>().isInObstacleArea = false;
                        //other.GetComponent<PlayerController>().ChangeDestination(nextDestination);
                        other.GetComponent<PlayerController>().SetAgentSpeed(25f);
                        Debug.Log("Slide!");
                    }
                    else
                    {
                        other.GetComponent<PlayerController>().isInObstacleArea = true;
                        other.GetComponent<PlayerController>().SetAgentSpeed(0f);
                    }
                        break;

                    case ObjectType.Above:
                    if (!other.GetComponent<PlayerController>().isTouchingGround)
                    {
                        other.GetComponent<PlayerController>().isInObstacleArea = false; 
                        //other.GetComponent<PlayerController>().ChangeDestination(nextDestination);
                        other.GetComponent<PlayerController>().SetAgentSpeed(25f);
                    }
                    else
                    {
                        other.GetComponent<PlayerController>().isInObstacleArea = true;
                        other.GetComponent<PlayerController>().SetAgentSpeed(0f);
                    }
                    break;
                }
          //  }
        }
    }
}
