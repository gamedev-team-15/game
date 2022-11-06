namespace ModVal
{
    [System.Serializable]
    public class ModifiableValueFloat : ModifiableValue<float>
    {
        public ModifiableValueFloat(float baseValue) : base(baseValue)
        {
        }

        protected override float Add(float a, float b)
        {
            return a + b;
        }

        protected override float Multiply(float a, float b)
        {
            return a * b;
        }

        protected override float Divide(float a, float b)
        {
            return a / b;
        }
    }
}