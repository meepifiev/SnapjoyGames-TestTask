using System;
using UnityEngine;

namespace Project.Scripts.Runtime.Core.Input
{
    public interface IInputReader
    {
        event Action<Vector2> MoveChanged;
        event Action<Vector2> LookChanged;
        
        event Action<Vector2> PointerDeltaChanged;
        
        event Action InteractionPressed;
        event Action InteractionHeld;
        event Action InteractionReleased;
        
        event Action PrimaryPointerPressed;
        event Action PrimaryPointerHeld;
        event Action PrimaryPointerReleased;

        Vector2 Move { get; }
        Vector2 Look { get; }
        
        Vector2 PointerDelta { get; }
        
        bool IsInteractionHeld { get; }
        bool IsPrimaryPointerHeld { get; }

        void Read();
    }
}
