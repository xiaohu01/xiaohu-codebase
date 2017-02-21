using System;

namespace CarFinder
{
    public class GoldenSectionCarFinder : CarFinderBase
    {
        public GoldenSectionCarFinder(Car car) : base(car)
        { }

        private bool firstTryOnInitPosition = true;

        protected override bool VelocityPredicted(int currentPosition)
        {
            double k = Math.Sqrt(5);
            Velocity = (int)(minVelocity * (k - 1) / (k + 1) + maxVelocity * 2 / (k + 1));
            Position = InitialPosition + Velocity * Ticks;
            if (currentPosition > Position)
            {
                minVelocity = Velocity;
                return false;
            }
            else if(currentPosition < Position)
            {
                maxVelocity = Velocity;
                return false;
            }
            else
                return true;
        }

        protected override bool InitialPositionPredicted(int currentPosition)
        {
            if(firstTryOnInitPosition)
            {
                firstTryOnInitPosition = false;
                minVelocity--;
                maxVelocity--;
            }

            initPosSearchCount++;
            if (initPosSearchCount == 50)
            {
                minVelocity++;
                maxVelocity++;
                maxInitialPosition = 1000;
                minInitialPosition = -1000;
                initPosSearchCount = 0;
            }

            Velocity = minVelocity;
            double k = Math.Sqrt(5);
            InitialPosition = (int)(minInitialPosition * (k - 1) / (k + 1) + maxInitialPosition * 2 / (k + 1));
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
