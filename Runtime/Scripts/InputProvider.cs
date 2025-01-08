using InputManagement;

public class InputProvider
{

    #region Input Receiver Assembly Line

    public int RequiredPriority { get; private set; }

    /// <summary>
    /// All input receivers bound to this buttonInput
    /// </summary>
    private PriorityGrouper<InputListener> inputReceivers = new PriorityGrouper<InputListener>();

    public void AddInputReceiver(InputListener receiver)
    {
        inputReceivers.AddItem(receiver);
        receiver.BindToInput(this);

        UpdateReceiverEnableStates();
    }

    public void RemoveInputReceiver(InputListener receiver)
    {
        inputReceivers.RemoveItem(receiver);
        receiver.Unbind(this);

        UpdateReceiverEnableStates();
    }

    /// <summary>
    /// Let ONLY each inputReceiver with the highest priority process the input
    /// </summary>
    public void UpdateReceiverEnableStates()
    {
        var highestPriorityReceivers = inputReceivers.GetHighestPriorityItems(out int maxPriority);
        RequiredPriority = maxPriority;

        foreach (InputListener input in inputReceivers.Items)
        {
            if (highestPriorityReceivers.Contains(input))
            {
                input.Active = true;
            }
            else
            {
                input.Active = false;
            }
        }
    }

    #endregion

}
