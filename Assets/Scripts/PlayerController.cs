using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [System.Serializable]
    public class MovementSettings
    {
        public float forwardVelocity = 10;
        public float jumpVelocity = 20;
        public int forwardInput = 1;
    }
    [System.Serializable]
    public class PhysicsSettings
    {
        public float downAccel = 0.75f;
    }


    public MovementSettings movementSettings = new MovementSettings();
    public PhysicsSettings physicsSettings = new PhysicsSettings();
    public float Xspeed = 10;
    public bool IsDead=false;
    public float fVelocity
    {
        get
        {
            return _fVelocity;
        }
        set
        {
            _fVelocity = value;
        }
      
    }
    public bool LevelStart = false;

    private Vector3 _velocity;
    private Rigidbody _ridgidbody;
    private Animator _animator;
    private int _jumpInput = 0, _slideInput = 0;
    private bool _onGround=false;
    private float _Xmovement=0;
    private CapsuleCollider _collider;
    private float _colliderSize;
    private float _colliderHight;
    private Vector3 _colliderCenter;
    private float _fVelocity;


	// Use this for initialization
	void Start () {
        _ridgidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _velocity = Vector3.zero;
        _collider = GetComponent<CapsuleCollider>();
        _colliderHight = _collider.height;
        _colliderCenter = _collider.center;
        _fVelocity = movementSettings.forwardVelocity;
	}
	

    void FixedUpdate()
    {
        if (LevelStart == true)
        {
            InputHandling();
            TouchHandling();
            Run();
            CheckGround();
            ColliderHandling();
            Jump();
            Slide();
            MoveX();
            _ridgidbody.velocity = _velocity * movementSettings.forwardInput;
        }
    }

    void ColliderHandling()
    {
        if (_onGround)
        {
            _colliderSize = _animator.GetFloat("ColliderSize");
            if (_colliderSize > 0.2f && _colliderSize < 1) // Shrink
            {
                movementSettings.forwardVelocity = _fVelocity + 5;
                _collider.height = 2;
                _collider.center = new Vector3(_collider.center.x, 0.85f, _collider.center.z);
            }
            else // Reset
            {
                _collider.height = _colliderHight;
                _collider.center = _colliderCenter;
                movementSettings.forwardVelocity = _fVelocity;
            }
        }
        else
        {
            _colliderSize = _animator.GetFloat("ColliderSize");
            if (_colliderSize > 0.1f && _colliderSize < 1) // Shrink
            {
                movementSettings.forwardVelocity = _fVelocity +5;
                _collider.height = 3.2f;
                _collider.center = new Vector3(_collider.center.x, 4.2f, _collider.center.z);
            }
            else // Reset
            {
                movementSettings.forwardVelocity = _fVelocity;
                _collider.height = _colliderHight;
                _collider.center = _colliderCenter;
            }
        }
    }

    void Run()
    {
        _velocity.z = movementSettings.forwardVelocity;
    }

    void Slide()
    {
        if (_slideInput == 1 && _onGround)
        {
            _animator.SetTrigger("Slide");
            _slideInput = 0;
        }
    }

    void Jump()
    {
        if (_jumpInput == 1 && _onGround)
        {
            _velocity.y = movementSettings.jumpVelocity;
            _animator.SetTrigger("Jump");
           
        }
        else if (_jumpInput == 0 && _onGround)
        {
            _velocity.y = 0;
          
        }
        else
        {
            _velocity.y -= physicsSettings.downAccel;
        }
        _jumpInput = 0;
    }
   
    void CheckGround()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, 0.5f);
        _onGround = false;
        _ridgidbody.useGravity = true;
        foreach (var hit in hits)
        {
            if (!hit.collider.isTrigger)
            {
                if (_velocity.y <= 0)
                {
                    _ridgidbody.position = Vector3.MoveTowards(_ridgidbody.position,
                        new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z), Time.deltaTime * 10);
                }
                _ridgidbody.useGravity = false;
                _onGround = true;
                break;
            }

        }
    }

    void MoveX()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_Xmovement, transform.position.y, transform.position.z), Time.deltaTime * Xspeed);
    }
    void InputHandling()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Jump
        {
            _jumpInput = 1;
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Slide
        {
            _slideInput = 1;
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Right
        {
            if (_Xmovement == 0) // player is in the middle
            {
                _Xmovement = 5;
                _animator.SetTrigger("RightMove");
            }
            else if (_Xmovement == -5) // player is in the left
            {
                _Xmovement = 0;
                _animator.SetTrigger("RightMove");
            }
        }
        else if (Input.GetKeyDown(KeyCode.A)) // left
        {
            if (_Xmovement == 0) // player is in the middle
            {
                _Xmovement = -5;
                _animator.SetTrigger("LeftMove");
            }
            else if (_Xmovement == 5) // player is in the right
            {
                _Xmovement = 0;
                _animator.SetTrigger("LeftMove");
            }
        }
    }

    private Touch _sTouch; bool _hasSwiped = false;

    void TouchHandling()
    {
        foreach (Touch t in Input.touches)
        {
            //Begin
            if (t.phase == TouchPhase.Began)
            {
                _sTouch = t;
            }
            // Moves
            else if (t.phase == TouchPhase.Moved && !_hasSwiped)
            {
                float Xswipe = _sTouch.position.x - t.position.x;
                float Yswipe = _sTouch.position.y - t.position.y;
                float Distance = Mathf.Sqrt((Xswipe * Xswipe)) + Mathf.Sqrt((Yswipe * Yswipe));
                bool IsVertical = false;
                if (Mathf.Abs(Xswipe) < Mathf.Abs(Yswipe))
                {
                    IsVertical = true;
                }
                if (Distance > 5f)
                {
                    if (IsVertical)
                    {
                        if (Yswipe < 0) // Jump
                        {
                            _jumpInput = 1;
                        }
                        else if (Yswipe > 0) //Slide
                        {
                            _slideInput = 1;
                        }
                    }
                    else if (!IsVertical)
                    {
                        if (Xswipe < 0) //Right 
                        {
                            if (_Xmovement == 0) // player is in the middle
                            {
                                _Xmovement = 5;
                                _animator.SetTrigger("RightMove");
                            }
                            else if (_Xmovement == -5) // player is in the left
                            {
                                _Xmovement = 0;
                                _animator.SetTrigger("RightMove");
                            }
                        }
                        else if (Xswipe > 0) //Left 
                        {
                            if (_Xmovement == 0) // player is in the middle
                            {
                                _Xmovement = -5;
                                _animator.SetTrigger("LeftMove");
                            }
                            else if (_Xmovement == 5) // player is in the right
                            {
                                _Xmovement = 0;
                                _animator.SetTrigger("LeftMove");
                            }
                        }
                    }
                    _hasSwiped = true;
                    
                }

            }
            //End
            else if (t.phase == TouchPhase.Ended)
            {
                _sTouch = new Touch();
                _hasSwiped = false;
            }
        }
    }
  }
