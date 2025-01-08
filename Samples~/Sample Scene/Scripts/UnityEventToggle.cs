using InputManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnityEventToggle : MonoBehaviour
{
    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;
    [SerializeField] private UnityEvent OnEnable;
    [SerializeField] private UnityEvent OnDisable;
    private bool active;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        active = false;
        OnDisable.Invoke();
        image.color = disabledColor;
        InputManager.Instance.Input.interact.OnPressEvent += Interact_OnPressEvent;
    }

    private void Interact_OnPressEvent(object sender, System.EventArgs e)
    {
        ToggleState();
    }

    public void ToggleState()
    {
        active = !active;
        if (active)
        {
            OnEnable.Invoke();
            image.color = enabledColor;
        }
        else
        {
            OnDisable.Invoke();
            image.color = disabledColor;
        }
    }
}
