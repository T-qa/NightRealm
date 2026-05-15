/// <summary>
/// Interface for objects that can be interacted with
/// Moved from distributed locations to centralize interaction contracts
/// </summary>
public interface IInteractable
{
    void Interact();
    bool CanInteract();
    string GetInteractionPrompt();
}
