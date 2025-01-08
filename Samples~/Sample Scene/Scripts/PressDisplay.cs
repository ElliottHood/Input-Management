using InputManagement;
using UnityEngine;
using UnityEngine.UI;

public class PressDisplay : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color green;

    private void Update()
    {
        bool isJumpPressed = InputManager.Instance.Input.jump.GetHeld();
        image.color = isJumpPressed ? green : Color.white;
    }
}
