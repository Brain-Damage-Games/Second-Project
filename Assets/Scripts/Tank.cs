using UnityEngine;
using UnityEngine.Events;

public class Tank : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private Transform tank;
    //remove comment from the SERIALIZEDFIELD below if you wanna test havinf a target dont forget to make the hasTarget bool true
    //[SerializeField]
    private Transform target;
    [SerializeField]
    private Transform bulletShootPoint;

    [Header("Rotation Setting")]
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private float patrolRotationSpeed = 5f;
    [SerializeField]
    private float minPatrolDegree = 25f; 
    [SerializeField]
    private float maxPatrolDegree = 80f;


    [Header("Shooting Setting")]
    [SerializeField]
    private float tankShooterShakePower = 0.2f;
    [SerializeField]
    private float tankShakePower = 0.1f;
    [SerializeField]
    float limitedAngleToShoot = 0.2f;
    [SerializeField]
    private AnimationCurve MoveCurve1;
    [SerializeField]
    private AnimationCurve MoveCurve2;
    [SerializeField]
    private Shooting shooting;




    private bool goRight = true;
    private float patrolDegree;
    private Transform tankShooter;
    private bool hasTarget = true;
    private bool fixedOnTraget = false;
    private bool shake = false;
    private Vector3 tOrigin1;
    private Vector3 tOrigin2;
    private Vector3 tTarget1;
    private Vector3 tTarget2;
    private float _animationTimePosition;

    void Awake()
    {
        tankShooter = tank.GetChild(0);
        patrolDegree = Random.Range(minPatrolDegree, maxPatrolDegree);
        shooting.OnShoot.AddListener(ShootProperty);
        shooting.SetShootTarget(target);
        
    }
    
    void Update()
    {
        TankManager();
    }
    private void TankManager()
    {
        _animationTimePosition += Time.deltaTime;
        if (hasTarget)
        {
            RotateToTarget();
            if (fixedOnTraget)
            {
                shooting.SetShooting(true);
                shooting.SetShootTarget(target);
            }
            else
                shooting.SetShooting(false);
        }
        else
        {
            Patrol();
            shooting.SetShooting(false);
        }

        if (shake)
        {
            Shake();
        }
    }
    private void RotateToTarget()
    {
        Vector3 targetRotation = target.position - tankShooter.position;
        Vector3 checkRotation = bulletShootPoint.position - tankShooter.position;

        //here I made the rotation only available for  y axes so the tank won't collapse into the ground or sky :D
        targetRotation.y= 0;
        checkRotation.y = 0;

        tankShooter.rotation = Quaternion.Slerp(tankShooter.rotation, Quaternion.LookRotation(targetRotation), rotationSpeed * Time.deltaTime);
        fixedOnTraget = Vector3.Angle(targetRotation, checkRotation) <= limitedAngleToShoot;

    }
    private void Patrol()
    {
        float rotatedDegree = tankShooter.eulerAngles.y - tank.eulerAngles.y;

        if (rotatedDegree > 180)
            rotatedDegree = rotatedDegree - 360;

        if (goRight && rotatedDegree >= patrolDegree)
        {
            patrolDegree = Random.Range(minPatrolDegree, maxPatrolDegree);
            goRight = false;
        }
        else if (!goRight && rotatedDegree <= -patrolDegree)
        {
            patrolDegree = Random.Range(minPatrolDegree, maxPatrolDegree);
            goRight = true;
        }

        if (goRight)
        {
            float targetAngle = patrolDegree+2;
            tankShooter.rotation = Quaternion.Slerp(tankShooter.rotation, Quaternion.Euler(0, targetAngle,0 ), patrolRotationSpeed * Time.deltaTime);
        }
        else
        {
            float targetAngle = -patrolDegree-2;
            tankShooter.rotation = Quaternion.Slerp(tankShooter.rotation, Quaternion.Euler(0, targetAngle, 0), patrolRotationSpeed * Time.deltaTime);
        }
    }
    private void ShootProperty()
    {
        Vector3 dir = bulletShootPoint.position - tankShooter.position;
        dir.y = 0;
        dir = dir.normalized;

        tOrigin2 = tank.position;
        tOrigin1 = tankShooter.position;
        tTarget2 = tank.position - dir * tankShakePower;
        tTarget1 = tankShooter.position - dir * tankShooterShakePower;

        shake = true;
        _animationTimePosition = 0;
    }
    private void Shake()
    {
        if (_animationTimePosition >= 1)
        {
            shake = false;
            _animationTimePosition = 0;
            return;
        }
        tankShooter.position = Vector3.Lerp(tOrigin1, tTarget1, MoveCurve1.Evaluate(_animationTimePosition));
        tank.position = Vector3.Lerp(tOrigin2, tTarget2, MoveCurve2.Evaluate(_animationTimePosition));
    }
    public void SetTarget(Transform target) 
    {
        hasTarget = true;
        this.target = target;
    }
    public void RemoveTarget()
    {
        hasTarget = false;
    }

}
