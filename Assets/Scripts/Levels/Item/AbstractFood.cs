using System.Collections;
using System.Collections.Generic;
using Tools.GameProgrammingPatterns.Command;
using UnityEngine;

public abstract class AbstractFood : MonoBehaviour, ICommand
{
    public virtual void Execute()
    {
        DisableThings();
    }

    public virtual void Undo()
    {
        EnableThings();
    }

    private void DisableThings()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<Collider2D>().enabled = false;
    }

    private void EnableThings()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        GetComponentInChildren<Collider2D>().enabled = true;
    }
}
