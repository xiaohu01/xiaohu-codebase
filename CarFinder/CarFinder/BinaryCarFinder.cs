namespace CarFinder
{
    public class BinaryCarFinder : CarFinderBase
    {
        public BinaryCarFinder(Car car) : base(car)
        { }

        protected override bool VelocityPredicted(int currentPosition)
        {
            Velocity = (minVelocity + maxVelocity) / 2;
            Position = InitialPosition + Velocity * Ticks;
            if (currentPosition > Position)
            {
                minVelocity = Velocity;
                return false;
            }
            else if (currentPosition < Position)
            {
                maxVelocity = Velocity;
                return false;
            }
            else
                return true;
        }

        protected override bool InitialPositionPredicted(int currentPosition)
        {
            initPosSearchCount++;
            if (initPosSearchCount == 20)
            {
                minVelocity++;
                maxVelocity++;
                maxInitialPosition = 1000;
                minInitialPosition = -1000;
                initPosSearchCount = 0;
            }

            Velocity = minVelocity;
            InitialPosition = (minInitialPosition + maxInitialPosition) / 2;
            Position = InitialPosition + Velocity * Ticks;
            if (currentPosition > Position)
            {
                minInitialPosition = InitialPosition;
                return false;
            }
            else if (currentPosition < Position)
            {
                maxInitialPosition = InitialPosition;
                return false;
            }
            else
                return true;
        }
    }
}
