using InputManagement;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class ChargeUpCircle : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private static readonly int ChargeUp = Animator.StringToHash("ChargeUp");
    private static readonly int Success = Animator.StringToHash("Success");
    private static readonly int Cancel = Animator.StringToHash("Cancel");

    private void OnEnable()
    {
        animator.enabled = false;
        GetComponent<SpriteRenderer>().sprite = null;
    }


    #region Displaying On Input

    [SerializeField] private ChargeInputReceiver chargeInputReceiver;

    private void Awake()
    {
        chargeInputReceiver.OnChargeStart += chargeInputReceiver_OnChargeStart;
        chargeInputReceiver.OnChargeCancel += chargeInputReceiver_OnChargeCancel;
        chargeInputReceiver.OnExecuteInput += chargeInputReceiver_OnExecuteInput;
    }

    private void chargeInputReceiver_OnChargeStart(object sender, ChargeInputEventArgs e)
    {
        animator.enabled = true;
        animator.speed = 1 / e.duration;

        animator.CrossFadeInFixedTime(ChargeUp, 0, 0);
    }

    private void chargeInputReceiver_OnChargeCancel(object sender, ChargeInputEventArgs e)
    {
        if (!animator.gameObject.activeInHierarchy)
            return;
        animator.speed = 1;
        animator.CrossFadeInFixedTime(Cancel, 0, 0);
    }

    private void chargeInputReceiver_OnExecuteInput(object sender, System.EventArgs e)
    {
        animator.speed = 1;
        animator.CrossFadeInFixedTime(Success, 0, 0);
    }

    #endregion

}
