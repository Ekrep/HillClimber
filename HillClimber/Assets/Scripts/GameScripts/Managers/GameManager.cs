using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private void Awake()
    {
        Instance = this;
    }



    public static event Action OnGrabbableObjectSelected;
    public static event Action OnCharacterDead;

    public void GrabbableObjectSelected()
    {
        if (OnGrabbableObjectSelected!=null)
        {
            OnGrabbableObjectSelected();
        }
    }
    public void CharacterDead()
    {
        if (OnCharacterDead!=null)
        {
            OnCharacterDead();
        }
    }
}
