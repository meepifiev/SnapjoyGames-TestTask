using System;
using MessagePipe;
using Project.Scripts.Runtime.Features.Messages;

namespace Project.Scripts.Runtime.Features.Player
{
    public class PlayerModeHandler : IDisposable
    {
        private readonly PlayerViewLock _viewLock;
        private readonly PlayerCursor _cursor;
        
        private readonly IDisposable _subscriptions;

        public PlayerModeHandler(
            PlayerViewLock viewLock,
            PlayerCursor cursor,
            ISubscriber<M_PlayerMovementLockChanged> movementLockSubscriber,
            ISubscriber<M_PlayerLookLockChanged> lookLockSubscriber,
            ISubscriber<M_PlayerInteractionLockChanged> interactionLockSubscriber,
            ISubscriber<M_PlayerCursorVisibilityChanged> cursorVisibilitySubscriber)
        {
            _viewLock = viewLock ?? throw new ArgumentNullException(nameof(viewLock));
            _cursor = cursor ?? throw new ArgumentNullException(nameof(cursor));

            if (movementLockSubscriber == null)
                throw new ArgumentNullException(nameof(movementLockSubscriber));

            if (lookLockSubscriber == null)
                throw new ArgumentNullException(nameof(lookLockSubscriber));

            if (interactionLockSubscriber == null)
                throw new ArgumentNullException(nameof(interactionLockSubscriber));

            if (cursorVisibilitySubscriber == null)
                throw new ArgumentNullException(nameof(cursorVisibilitySubscriber));

            _subscriptions = DisposableBag.Create(
                movementLockSubscriber.Subscribe(ApplyMovementLock),
                lookLockSubscriber.Subscribe(ApplyLookLock),
                interactionLockSubscriber.Subscribe(ApplyInteractionLock),
                cursorVisibilitySubscriber.Subscribe(ApplyCursorVisibility));
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
        }

        private void ApplyMovementLock(M_PlayerMovementLockChanged message)
        {
            if (message.IsLocked)
            {
                _viewLock.LockMovement();
                return;
            }

            _viewLock.UnlockMovement();
        }

        private void ApplyLookLock(M_PlayerLookLockChanged message)
        {
            if (message.IsLocked)
            {
                _viewLock.LockLook();
                return;
            }

            _viewLock.UnlockLook();
        }

        private void ApplyInteractionLock(M_PlayerInteractionLockChanged message)
        {
            if (message.IsLocked)
            {
                _viewLock.LockInteraction();
                return;
            }

            _viewLock.UnlockInteraction();
        }

        private void ApplyCursorVisibility(M_PlayerCursorVisibilityChanged message)
        {
            if (message.IsVisible)
            {
                _cursor.RequestVisible();
                return;
            }

            _cursor.ReleaseVisible();
        }
    }
}
