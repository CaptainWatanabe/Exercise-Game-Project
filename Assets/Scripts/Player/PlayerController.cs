using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

public enum PlayerIngameType { Running, Fighting}

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerIngameType inGameType;
    
    [Header("References")]
    [SerializeField] Animator anim;
    [SerializeField] NavMeshAgent agent;

    [Header("Mevement Settings")]
    [SerializeField] Transform destinationPos;

    float runSpeed;
    public bool isNotDefaultAnimation => ( isSliding || !isTouchingGround );
    public bool isSliding;
    public bool isTouchingGround = true;
    public bool isInObstacleArea = false;

    private void Start()
    {
        InGameInput.inputInstance.onSwingUp += Jump;
        InGameInput.inputInstance.onSwingDown += Sliding;

        agent.updateRotation = false;
        agent.SetDestination(destinationPos.position);
    }

    private void OnDisable()
    {
        InGameInput.inputInstance.onSwingUp -= Jump;
        InGameInput.inputInstance.onSwingDown -= Sliding;
    }

    void Update()
    {
        if (inGameTracker.trackerInstance.gameState == GameState.Stop)
        {
            agent.speed = 0;
            anim.SetFloat("Run Power", 0);
            if (inGameTracker.trackerInstance.isGameFinished)
                anim.SetBool("IsFinished", true);
            return;
        }

        agent.speed =  InGameInput.inputInstance.runningPower * 3;
        anim.SetFloat("Run Power", InGameInput.inputInstance.runningPower);

        var turnTowardNavSteeringTarget = agent.steeringTarget;

        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

        ////Unity Editor Below

        //if (Input.GetKey(KeyCode.D))
        //    runSpeed = 2;
        //else if (Input.GetKeyUp(KeyCode.D))
        //    runSpeed = 0;

        //if (Input.GetKeyDown(KeyCode.Space))
        //    Jump();

        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //    Sliding();

        //anim.SetFloat("Run Power", runSpeed);
        //agent.speed = !isInObstacleArea ? runSpeed * 5 : agent.speed;

    }

    void Jump()
    {

        if (!isNotDefaultAnimation)
        {
            isTouchingGround = false;

            anim.SetTrigger("Jump");
        }
    }

    void Sliding()
    {
        if (!isNotDefaultAnimation)
        {
            isSliding = true;
            anim.SetTrigger("Slide");
        }

    }

    public void TouchGround()
    {
        agent.speed = 0;
        agent.acceleration = 10f;
        agent.velocity = Vector3.zero;
        isTouchingGround = true;
        isSliding = false;
    }

    public void ChangeDestination(Transform _newDestination)
    {
        agent.SetDestination(_newDestination.position);
    }

    public void SetAgentSpeed(float _speed)
    {
        agent.speed = _speed;
        agent.acceleration = _speed <= 0? 10 : _speed;

        if(_speed <= 0)
        {
            agent.velocity = Vector3.zero;
        }
        //agent.velocity = Vector3.forward * _speed;
    }
}
