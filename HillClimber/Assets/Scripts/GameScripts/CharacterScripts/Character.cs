using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character Instance;

    public CharacterStats characterStat;

    private Enums.CharacterStates _characterCurrentState;

    [SerializeField] private Rigidbody _rightArm;

    [SerializeField] private Rigidbody _leftArm;

    private float _power;

    private float _flyableDistance;

    #region GetSet
   
    public float CharacterPower
    {
        get
        {
            return _power;
        }
    }
    public float FlyAbleDistance
    {
        get
        {
            return _flyableDistance;
        }
    }

    public Enums.CharacterStates CharacterCurrentState
    {
        get
        {
            return _characterCurrentState;
        }
        set
        {
            _characterCurrentState = value;
        }
    }
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        GameManager.OnGrabbableObjectSelected += GameManager_OnGrabbableObjectSelected;
        GameManager.OnCharacterDead += GameManager_OnCharacterDead;
    }

    private void GameManager_OnCharacterDead()
    {
        _characterCurrentState = Enums.CharacterStates.Dead;
    }

    private void GameManager_OnGrabbableObjectSelected()
    {
        _characterCurrentState = Enums.CharacterStates.MidAir;
    }
    private void OnDisable()
    {
        GameManager.OnGrabbableObjectSelected -= GameManager_OnGrabbableObjectSelected;
        GameManager.OnCharacterDead -= GameManager_OnCharacterDead;
    }

    private void Start()
    {
        _power = characterStat.characterPower;
        _flyableDistance = characterStat.characterFlyableRange;
        _characterCurrentState = Enums.CharacterStates.Alive;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_characterCurrentState!=Enums.CharacterStates.Dead)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                GameManager.Instance.CharacterDead();
            }
        }
        
    }


}
