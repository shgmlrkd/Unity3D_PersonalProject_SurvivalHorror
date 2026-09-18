using System;

public interface IInteractState
{
    event Action OnChanged;
}