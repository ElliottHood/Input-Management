using System;
using UnityEngine;

namespace InputManagement
{
    /// <summary>
    /// An input receiver that implements charge and release functionality
    /// </summary>
    public class ChargeInputReceiver : ButtonInputReceiver
    {
        [SerializeField] private float chargeTime = 0.3f;
        [Tooltip("The charge will automatically complete if there is less than this amount of time left when the button is released")]
        [SerializeField] private float chargeReleaseBufferTime = 0.1f;
        private bool charging = false;
        protected Timer timer = new Timer();
        public float progress => timer.progress;

        public event EventHandler<ChargeInputEventArgs> OnChargeStart;
        public event EventHandler<ChargeInputEventArgs> OnChargeCancel;

        protected override void InterceptInput()
        {
            // The buffer threshold 
            if (HandleReleaseBufferInputOverride())
                return;

            // Starting and releasing charge
            if (buttonInputProvider.GetHeld(priority) && !charging)
            {
                StartCharge();
            }
            else if (buttonInputProvider.GetReleasedThisFrame(priority) && charging)
            {
                ReleaseChargeInput();
            }
        }

        private void StartCharge()
        {
            charging = true;
            timer.Start(chargeTime);

            OnChargeStart?.Invoke(this, new ChargeInputEventArgs
            {
                duration = chargeTime,
            });
        }

        protected virtual void ReleaseChargeInput()
        {
            // Override player input and start a buffer to finish the charge if its almost completed
            if (bufferOverrideApplicable)
            {
                StartChargeBuffer();
                return;
            }

            if (timer.finished)
            {
                FullyReleaseCharge();
            }
            else
            {
                CancelCharge();
            }
        }

        private void FullyReleaseCharge()
        {
            charging = false;
            ResolveInput();
        }

        private void CancelCharge()
        {
            charging = false;
            OnChargeCancel?.Invoke(this, new ChargeInputEventArgs
            {
                duration = chargeTime,
            });
        }

        protected override void OnInputReceivingDisabled()
        {
            CancelCharge();
        }


        #region Release Buffer

        private bool bufferOverrideActive;
        private bool bufferOverrideApplicable => !timer.finished && timer.timeUntilCompletion < chargeReleaseBufferTime;

        /// <summary>
        /// If the player releases the button when they are almost fully charged, complete the rest of the charge automatically for them
        /// </summary>
        /// <returns></returns>
        private bool HandleReleaseBufferInputOverride()
        {
            if (timer.finished && bufferOverrideActive)
            {
                ReleaseChargeInput();
                bufferOverrideActive = false;
            }

            return bufferOverrideActive;
        }

        /// <summary>
        /// If the player releases the charge when they are almost fully charged, complete the rest of the charge automatically for them
        /// </summary>
        /// <returns></returns>
        private void StartChargeBuffer()
        {
            bufferOverrideActive = true;
        }

        #endregion

    }

    public class ChargeInputEventArgs : EventArgs
    {
        public float duration;
    }
}
