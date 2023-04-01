namespace Akrab
{
    public abstract class CustomBehaviour : UpdateBehaviour
    {
        bool isInit = false;
        private void Awake()
        {
            Setup();
        }

        public void Setup()
        {
            if (isInit)
                return;

            setup();
            isInit = true;
        }

        protected virtual void setup()
        {

        }
    }
}
