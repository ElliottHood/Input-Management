using InputManagement;
using System;
using System.Collections;
using UnityEngine;

public abstract class InputListener : MonoBehaviour, IPriorityGroupable
{
    [SerializeField] protected int priority = 0;
    protected InputProvider inputProvider;

    #region Enabling / Disabling the Input Listener

    public event EventHandler OnEnabled;
    public event EventHandler OnDisabled;
    protected virtual void OnInputReceivingEnabled() { }
    protected virtual void OnInputReceivingDisabled() { }

    private bool active;
    public bool Active
    {
        get
        {
            return active;
        }
        set
        {
            value = value && inputProvider != null && GetRetrievable();

            if (active == value)
                return;

            active = value;

            if (active)
            {
                print($"{gameObject} is active");
                StopAllCoroutines();
                StartCoroutine(InterceptInputCoroutine());

                OnEnabled?.Invoke(this, EventArgs.Empty);
                OnInputReceivingEnabled();
            }
            else
            {
                StopAllCoroutines();

                OnDisabled?.Invoke(this, EventArgs.Empty);
                OnInputReceivingDisabled();
            }
        }
    }

    #endregion

    /// <summary>
    /// Handles implementation of input gestures: double tapping, charging, etc...
    /// MAKE SURE TO GET INPUTS USING THIS OBJECT'S PRIORITY ex. inputProvider.GetValue(priority);
    /// </summary>
    protected abstract void InterceptInput();

    #region Intercepting Input

    public void BindToInput(InputProvider inputProvider)
    {
        this.inputProvider = inputProvider;
        Active = true;
    }

    private IEnumerator InterceptInputCoroutine()
    {
        while (Active)
        {
            InterceptInput();
            yield return null;
        }
    }

    public void Unbind(InputProvider inputProvider)
    {
        this.inputProvider = null;
        Active = false;
    }

    #endregion


    #region IPriority Groupable

    public int GetPriority()
    {
        return priority;
    }

    public bool GetRetrievable()
    {
        return enabled && gameObject != null && gameObject.activeInHierarchy;
    }

    #endregion


    #region Enabling / Disabling

    private void OnEnable()
    {
        if (inputProvider != null && GetRetrievable())
        {
            inputProvider.UpdateReceiverEnableStates();
        }
    }

    private void OnDisable()
    {
        if (this != null && gameObject != null && inputProvider != null)
        {
            inputProvider.UpdateReceiverEnableStates();
        }
    }

    #endregion
}
