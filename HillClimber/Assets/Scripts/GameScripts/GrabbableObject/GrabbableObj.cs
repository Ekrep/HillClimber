using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObj : MonoBehaviour
{
    private SpringJoint _springJoint;
    private FixedJoint _fixedJoint;
    private bool _isGrabbed;
    private bool _isGrabbable;
    #region Get Set
    public SpringJoint GrabbableObjectSpringJoint
    {
        get
        {
            return _springJoint;
        }
    }
    public bool IsGrabbable
    {
        get
        {
            return _isGrabbable;
        }
        set
        {
            _isGrabbable = value;
        }
    }
    #endregion
    private void OnEnable()
    {
        GameManager.OnGrabbableObjectSelected += GameManager_OnGrabbableObjectSelected;
        GameManager.OnCharacterDead += GameManager_OnCharacterDead;
    }

    private void GameManager_OnCharacterDead()
    {
        _springJoint.connectedBody = null;
        _fixedJoint.connectedBody = null;
        _isGrabbed = false;
        _isGrabbable = false;
        StopCoroutine(PullUntilGrab());
    }

    private void GameManager_OnGrabbableObjectSelected()
    {
        _springJoint.maxDistance = Vector3.Distance(gameObject.transform.position, Character.Instance.transform.position);
        if (_isGrabbed)
        {
            _springJoint.connectedBody = null;
            _fixedJoint.connectedBody = null;
            _isGrabbed = false;
            _isGrabbable = false;
            StartCoroutine(ActivateGrabbableObject());
        }
        
        
    }
    private void OnDisable()
    {
        GameManager.OnGrabbableObjectSelected -= GameManager_OnGrabbableObjectSelected;
        GameManager.OnCharacterDead -= GameManager_OnCharacterDead;
    }

    private void Start()
    {
        _isGrabbable = true;
        _springJoint = GetComponent<SpringJoint>();
        _fixedJoint = GetComponent<FixedJoint>();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.GetComponent<Hand>()!=null)
        {
            if (_isGrabbable&&SelectionManager.Instance.selectedGrabbableObj==this.gameObject)
            {
                if (Character.Instance.CharacterCurrentState == Enums.CharacterStates.MidAir)
                {
                    _fixedJoint.connectedBody = collision.gameObject.GetComponent<Rigidbody>();
                    _isGrabbed = true;
                    Character.Instance.CharacterCurrentState = Enums.CharacterStates.Alive;

                }
            }
           
           
            
        }
    }

    public void PullCharacter()
    {
        StartCoroutine(PullUntilGrab());
    }
    
    IEnumerator PullUntilGrab()
    {
        yield return new WaitForEndOfFrame();
        if (!_isGrabbed&&!SelectionManager.Instance.selectedHand.IsGrabbed)
        {
            
            
            _springJoint.maxDistance = Mathf.MoveTowards(_springJoint.maxDistance, 0, Character.Instance.CharacterPower*0.1f * Time.deltaTime);
            StartCoroutine(PullUntilGrab());

        }
        else
        {
            StopCoroutine(PullUntilGrab());
        }

    }
    IEnumerator ActivateGrabbableObject()
    {
        yield return new WaitForSecondsRealtime(1f);
        _isGrabbable = true;
    }





}
