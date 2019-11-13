using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}

public class StateMachine
{
    IState currentState;

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if(currentState != null)
        {
            currentState.Execute();
        }
    }
}

public class State_Patrol : IState
{
    EnemyController owner;
    NavMeshAgent agent;
    Waypoint waypoint;
    public State_Patrol(EnemyController owner) { this.owner = owner; }
    public void Enter()
    {
        Debug.Log("entering patrol state");
        waypoint = owner.waypoint;
        agent = owner.GetComponent<NavMeshAgent>();
        agent.destination = waypoint.transform.position;
        // start moving, in case we were previously stopped
        agent.isStopped = false;
        agent.speed = owner.walkSpeed;
    }
    public void Execute()
    {
        Debug.Log("updating patrol state");
        // same as before
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Waypoint nextWaypoint = waypoint.nextWaypoint;
            waypoint = nextWaypoint;
            agent.destination = waypoint.transform.position;
        }

        if (owner.seenTarget)
        {
            owner.stateMachine.ChangeState(new State_Attack(owner));
        }
    }
    public void Exit()
    {
        Debug.Log("exiting patrol state");
        // stop moving
        agent.isStopped = true;
    }
}

public class State_Attack : IState
{
    EnemyController owner;
    NavMeshAgent agent;
    
    public State_Attack(EnemyController owner) { this.owner = owner; }
    public void Enter()
    {
        Debug.Log("entering attack state");
        agent = owner.GetComponent<NavMeshAgent>();
        agent.speed = owner.runSpeed;
        if (owner.seenTarget)
        {
            agent.destination = owner.lastSeenPosition;
            agent.isStopped = false;
            
        }
    }
    public void Execute()
    {
        Debug.Log("updating attack state");
        agent.destination = owner.lastSeenPosition;
        agent.isStopped = false;
        if (!agent.pathPending && agent.remainingDistance < 5.0f)
        {
            agent.isStopped = true;
        }
        if (owner.seenTarget != true)
        {
            Debug.Log("lost sight");
            // search for the player
            owner.stateMachine.ChangeState(new State_Searching(owner));
        }
        else
        {
            //Firing
            Debug.Log("Firing");
            if (Time.time > owner.nextFire)
            {
                owner.nextFire = Time.time + owner.fireRate;
                owner.fire();
            }
        }
    }

    public void Exit()
    {
        Debug.Log("exiting attack state");
        agent.isStopped = true;
    }
}

public class State_Searching : IState
{
    EnemyController owner;
    NavMeshAgent agent;
    public State_Searching(EnemyController owner) { this.owner = owner; }
    public void Enter()
    {
        Debug.Log("entering searching state");
        agent = owner.GetComponent<NavMeshAgent>();
        agent.destination = owner.lastSeenPosition;
        agent.speed = owner.runSpeed;
        agent.isStopped = false;
    }
    public void Execute()
    {
        Debug.Log("updating searching state");
        agent.destination = owner.lastSeenPosition;
        agent.isStopped = false;
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Debug.Log("give up finding target");
            owner.stateMachine.ChangeState(new State_Patrol(owner));
        }

        if (owner.seenTarget == true)
        {
            Debug.Log("re-found target");
            owner.stateMachine.ChangeState(new State_Attack(owner));

        }
    }

    public void Exit()
    {
        Debug.Log("exiting searching state");
        agent.isStopped = true;
    }
}

public class EnemyController : MonoBehaviour {

    public Waypoint waypoint;
    public GameObject target;
    NavMeshAgent agent;

    public float sightFov = 110;

    public float nextFire;
    public float fireRate = 1.25f;

    public float runSpeed = 7f;
    public float walkSpeed = 3f;

    public bool seenTarget = false;
    private SphereCollider collider;
    public Vector3 lastSeenPosition;

    public GameObject shot;
    public Transform shotTransform;

    public StateMachine stateMachine = new StateMachine();

    // Use this for initialization
    void Start()
    {
        stateMachine.ChangeState(new State_Patrol(this));

        //agent = GetComponent<NavMeshAgent>();
        //agent.destination = waypoint.transform.position;

         collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    public void fire()
    {
        GameObject shotFired = Instantiate(shot,shotTransform.position, Quaternion.LookRotation(lastSeenPosition -shotTransform.position));
        Debug.Log(shotFired.transform.forward);
    }

    private void OnTriggerStay(Collider other)
    {
        // is it the player?
        if (other.gameObject == target)
        {
            // angle between us and the player
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            // reset whether we’ve seen the player
            seenTarget = false;
            RaycastHit hit;
            // is it less than our field of view
            if (angle < sightFov * 0.5f)
            {
                // if the raycast hits the player we know
                // there is nothing in the way
                // adding transform.up raises up from the floor by 1 unit
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, collider.radius))
                {
                    if (hit.collider.gameObject == target)
                    {
                        // flag that we've seen the player
                        // remember their position
                        seenTarget = true;
                        lastSeenPosition = target.transform.position;
                    }


                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (collider != null)
        {
            Gizmos.DrawWireSphere(transform.position, collider.radius);
            if (seenTarget)
                Gizmos.DrawLine(transform.position, lastSeenPosition);
            if (lastSeenPosition != Vector3.zero)
            {
                Gizmos.DrawWireSphere(lastSeenPosition, 1);
            }

        }
    }
}
