using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private Transform tank;
    //remove comment from the SERIALIZEDFIELD below if you wanna test havinf a target dont forget to make the hasTarget bool true
    [SerializeField]
    private Transform target;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletShootPoint;

    [Header("Rotation Setting")]
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private float patrolRotationSpeed = 5f;
    [SerializeField]
    private float patrolDegree = 60f;

    [Header("Shooting Setting")]
    [SerializeField]
    private float coolDown = 1f;
    [SerializeField]
    private float shootPower = 100f;
    [SerializeField]
    private float tankShooterShakePower = 0.2f;
    //[SerializeField]
    //private float tankShakePower = 0.1f;
    [SerializeField]
    float limitedAngleToShoot = 0.2f;
    [SerializeField]
    private AnimationCurve MoveCurve1;




    private bool goRight = true;
    private Transform tankShooter;
    private bool hasTarget = false;
    private bool fixedOnTraget = false;
    private bool shake = false;
    private float passedTime = 0f;
    private Vector3 tOrigin;
    private Vector3 tTarget1;
    //private Vector3 tTarget2;
    private float _animationTimePosition;

    void Awake()
    {
        tankShooter = tank.GetChild(0);
        passedTime = coolDown;
    }
    
    void Update()
    {
        TankManager();
    }
    private void TankManager()
    {
        passedTime += Time.deltaTime;
        _animationTimePosition += Time.deltaTime;
        if (hasTarget)
        {
            RotateToTarget();
            if (passedTime >= coolDown && fixedOnTraget)
            {
                Shoot();
                passedTime = 0f;
            }
        }
        else
            Patrol();

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
            goRight = false;
        else if (!goRight && rotatedDegree <= -patrolDegree)
            goRight = true;

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
    private void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletShootPoint.position, Quaternion.identity, tank);
        Vector3 direction = (target.position - newBullet.transform.position).normalized;
        newBullet.GetComponent<Rigidbody>().AddForce(direction * shootPower);


        Vector3 dir = bulletShootPoint.position - tankShooter.position;
        dir.y = 0;
        dir = dir.normalized;
        tOrigin = tankShooter.position;
        tTarget1 = tankShooter.position - dir * tankShooterShakePower;
        //tTarget2 = tank.position - dir * tankShakePower;

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
        tankShooter.position = Vector3.Lerp(tOrigin, tTarget1, MoveCurve1.Evaluate(_animationTimePosition));
        //tank.position = Vector3.Lerp(tank.position, tTarget2, MoveCurve1.Evaluate(_animationTimePosition));
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
