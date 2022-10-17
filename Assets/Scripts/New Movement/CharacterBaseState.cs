using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseState
{
    public abstract void EnterState(CharacterStateManager state);

    public abstract void Update(CharacterStateManager state);

    public abstract void FixedUpdate(CharacterStateManager state);

    public abstract void OnCollisionEnter(CharacterStateManager state);

}
