using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private bool _isGrabbed;

    private GameObject _grabbedObject;

    #region Get Set
    public bool IsGrabbed
    {
        get
        {
            return _isGrabbed;
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
        _isGrabbed = false;
        if (_grabbedObject != null)
        {
            _grabbedObject = null;
        }
    }

    private void GameManager_OnGrabbableObjectSelected()
    {
        if (_grabbedObject!=null)
        {
            _isGrabbed = false;
        }
    }
    private void OnDisable()
    {
        GameManager.OnGrabbableObjectSelected -= GameManager_OnGrabbableObjectSelected;
        GameManager.OnCharacterDead -= GameManager_OnCharacterDead;
    }

    


    private void OnCollisionEnter(Collision collision)
    {
        if (!_isGrabbed)
        {
            if (Character.Instance.CharacterCurrentState==Enums.CharacterStates.MidAir)
            {
                if (collision.gameObject.GetComponent<GrabbableObj>() != null)
                {
                    if (collision.gameObject==SelectionManager.Instance.selectedGrabbableObj)
                    {
                        _grabbedObject = collision.gameObject;
                        _isGrabbed = true;
                    }

                    
                }
            }
                
            

        }
       
    }
  
}
