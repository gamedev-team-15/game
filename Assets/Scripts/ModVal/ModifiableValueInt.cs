namespace ModVal
{
    [System.Serializable]
    public class ModifiableValueInt : ModifiableValue<int>
    {
        public ModifiableValueInt(int baseValue) : base(baseValue)
        {
        }

        protected override int Add(int a, int b)
        {
            return a + b;
        }

        protected override int Multiply(int a, int b)
        {
            return a * b;
        }

        protected override int Divide(int a, float b)
        {
            return (int)(a / b);
        }
    }
}