namespace Input
{
    public interface IInputSystemListener
    {
        public void FireButtonPressed();
        public void FireButtonReleased();
        public void Interact();
        public void AbilityButtonPressed(int abilityId);
    }
}