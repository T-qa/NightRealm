public static class InputCentral
{
    private static InputActions _inputActions;

    public static InputActions InputActions => _inputActions ??= new();

    public static void Enable()
    {
        InputActions.Enable();
    }

    public static void Disable()
    {
        InputActions.Disable();
    }

    public static void DisablePlayerMovement()
    {
        InputActions.PlayerMove.Disable();
    }

    public static void DisablePlayerAbility()
    {
        InputActions.PlayerAbilityTrigger.Disable();
    }
}
