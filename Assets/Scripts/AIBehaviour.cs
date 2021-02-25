using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    public enum MovingState { None, MoveTowards, MoveAway }

    Rigidbody rb;

    public Transform[] targets;
    [SerializeField] Transform target;

    public float moveSpeed = 4;
    public float rotationSpeed = 100;

    public float preferredDistanceMin = 10f;
    public float preferredDistanceMax = 15f;

    MovingState movingState;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = targets[0];
    }

    private void FixedUpdate()
    {
        TargetChooser();

        rb.AddForce(new Vector3(-rb.velocity.x, 0, -rb.velocity.z), ForceMode.VelocityChange);

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            float preferredMiddle = (preferredDistanceMin + preferredDistanceMax) / 2f;

            if(distance > preferredDistanceMax)
            {
                movingState = MovingState.MoveTowards;
            } 
            else if (distance < preferredDistanceMin)
            {
                movingState = MovingState.MoveAway;
            } 
            else if(movingState == MovingState.MoveAway && distance > preferredMiddle)
            {
                movingState = MovingState.None;
            } 
            else if(movingState == MovingState.MoveTowards && distance < preferredMiddle)
            {
                movingState = MovingState.None;
            }

            Vector3 movement = new Vector3(0,0,0);
            if(movingState == MovingState.MoveTowards)
            {
                movement = (target.position - transform.position).normalized;
            }
            if (movingState == MovingState.MoveAway)
            {
                movement = (transform.position - target.position).normalized;
            }
            movement.y = 0;
            
            rb.AddForce(movement * moveSpeed, ForceMode.VelocityChange);

            Quaternion towardsTarget = Quaternion.LookRotation(target.position - transform.position);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, towardsTarget, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    void TargetChooser()
    {
        target = null;
        float bestScore = 32000f;
        for (int i = 0; i < targets.Length; i++)
        {
            float distanceScore = Vector3.Distance(transform.position, targets[i].position);
            Vector3 towardsTarget = targets[i].position - transform.position;
            float angleScore = Vector3.Angle(transform.forward, towardsTarget);

            float testScore = distanceScore * 10 + angleScore;

            if(testScore < bestScore)
            {
                bestScore = testScore;
                target = targets[i];
            }
        }

    }
}
