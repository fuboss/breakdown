using System;

namespace UnityCore.ReactiveProperties
{
    public class ReactiveButton : ReactiveProperty
    {
        public event Action OnClicked;
        public event Action OnHovered;
        public event Action OnUnhovered;
        public event Action OnPressed;
        public event Action OnReleased;
        public event Action OnHold;

        public void KillAllSubscribers()
        {
            if (OnClicked != null)
                foreach (var del in OnClicked?.GetInvocationList())
                    OnClicked -= (Action) del;
            
            if (OnHovered != null)
                foreach (var del in OnHovered?.GetInvocationList())
                    OnHovered -= (Action) del;
            
            if (OnUnhovered != null)
                foreach (var del in OnUnhovered?.GetInvocationList())
                    OnUnhovered -= (Action) del;
            
            if (OnPressed != null)
                foreach (var del in OnPressed?.GetInvocationList())
                    OnPressed -= (Action) del;
            
            if (OnReleased != null)
                foreach (var del in OnReleased?.GetInvocationList())
                    OnReleased -= (Action) del;
            
            if (OnHold != null)
                foreach (var del in OnHold?.GetInvocationList())
                    OnHold -= (Action) del;
        }
        
        public virtual void FireOnClicked()
        {
            OnClicked?.Invoke();
            NotifyChanged();
        }

        public virtual void FireOnHovered()
        {
            OnHovered?.Invoke();
            NotifyChanged();
        }

        public virtual void FireOnUnhovered()
        {
            OnUnhovered?.Invoke();
            NotifyChanged();
        }

        public virtual void FireOnPressed()
        {
            OnPressed?.Invoke();
            NotifyChanged();
        }

        public virtual void FireOnRelease()
        {
            OnReleased?.Invoke();
            NotifyChanged();
        }

        public virtual void FireOnHold()
        {
            OnHold?.Invoke();
            NotifyChanged();
        }
    }
}