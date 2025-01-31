using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControler : MonoBehaviour
{

    public Rigidbody _rb;

    private bool _Boost;




    [SerializeField]
    private float _speedMax = 3, _accelerationFactor, _decelerationFactor;
    public  float _speed = 0;
    public float _rotationSpeed = 0.5f;
    private bool _isAccelerating = false;
    [SerializeField]
    private float _accelerationLerpInterpolator;
    [SerializeField]
    private AnimationCurve _accelerationCurve;
    [SerializeField]
    private float _decelerationLerpInterpolator;
    [SerializeField]
    private AnimationCurve _decelerationCurve;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
        

        

        if (Input.GetKeyDown(KeyCode.Space))
        {

            _isAccelerating = true;
            //_rb.velocity = transform.forward * _acceleration;
            Debug.Log("vroum");

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isAccelerating = false;
            Debug.Log("Dévroum");
        }


        if (Input.GetKey(KeyCode.N))
        {
            if (!_Boost)
            {
                StartCoroutine(Boost());
            }
        }
        


        /*
                var xAngle = Mathf.Clamp(transform.eulerAngles.x+360, 320, 400);
                var yAngle = transform.eulerAngles.y;
                var zAngle = transform.eulerAngles.z;

                transform.eulerAngles = new Vector3(xAngle, yAngle, zAngle);
        */
    }


    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.D))
        {

            transform.eulerAngles += Vector3.up * _rotationSpeed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.eulerAngles += Vector3.down * _rotationSpeed;
        }


        if (_isAccelerating)
        {
            //_speed = Mathf.Clamp(_speed + _accelerationFactor * Time.fixedDeltaTime, 0, _speedMax);
            _accelerationLerpInterpolator += _accelerationFactor;
            _speed = _accelerationCurve.Evaluate(_accelerationLerpInterpolator) * _speedMax;

        }
        else
        {
            //_speed = Mathf.Clamp(_speed - _accelerationFactor * Time.fixedDeltaTime, 0, _speedMax);
            _accelerationLerpInterpolator -= _decelerationFactor;
            _speed = _decelerationCurve.Evaluate(_decelerationLerpInterpolator) * _speedMax;

        }
        _accelerationLerpInterpolator = Mathf.Clamp01(_accelerationLerpInterpolator);
        _decelerationLerpInterpolator = 1 - Mathf.Clamp01(_accelerationLerpInterpolator);

        

        _rb.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);
    }

    private IEnumerator Boost()
    {
        _Boost = true;
        _speedMax = 10;
        _accelerationFactor = _accelerationFactor * 10;
        

        yield return new WaitForSeconds(2);

        _speedMax = 3;
        _accelerationFactor = _accelerationFactor / 10;
        _Boost = false;

            

        
    }


}
