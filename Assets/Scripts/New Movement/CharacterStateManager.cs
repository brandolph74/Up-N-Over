using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    CharacterBaseState currentState;
    //public Land landState = new Land();
    //public Fly flyState = new Fly();

    private void Start()
    {
        //currentState = landState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.Update(this);
    }

    public void SwitchState(CharacterBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }
}
