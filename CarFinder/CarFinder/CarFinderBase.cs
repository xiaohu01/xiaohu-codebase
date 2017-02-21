using System;
using System.ComponentModel;

namespace CarFinder
{
    public abstract class CarFinderBase : INotifyPropertyChanged
    {
        #region protected fields
        protected int velocity;
        protected int initialPosition;
        protected int maxInitialPosition;
        protected int minInitialPosition;
        protected int maxVelocity;
        protected int minVelocity;
        protected int initPosSearchCount;
        #endregion

        #region private fields
        private Car _car;
        private int ticks;
        private bool findingCar;
        private bool carFound;
        private int position;
        #endregion

        #region constructor
        public CarFinderBase(Car car)
        {
            _car = car;
            _car.moved += _car_moved;
            Velocity = 0;
            InitialPosition = 0;
            Ticks = 0;
            CarFound = false;
            maxVelocity = 1000;
            minVelocity = -1000;
            maxInitialPosition = 1000;
            minInitialPosition = -1000;
            initPosSearchCount = 0;
            findingCar = false;
        }
        #endregion

        #region events and handlers
        public event EventHandler CarHasBeenFound;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private void _car_moved(object sender, EventArgs e)
        {
            Ticks++;
            if (findingCar && Ticks > 1000)
            {
                if(Predicted(_car.Position))
                {
                    CarFound = true;
                    _car.moved -= _car_moved;
                    CarHasBeenFound?.Invoke(this, new EventArgs());
                }
            }                
        }
        #endregion

        #region public members
        public void Find()
        {
            findingCar = true;
        }

        public void Cancel()
        {
            findingCar = false;
        }        

        public virtual string FinderName { get { return GetType().ToString().Split('.')[1]; } }

        public int Position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    position = value;
                    OnPropertyChanged("Position");
                }
            }
        }

        public bool CarFound
        {
            get { return carFound; }
            set
            {
                if (carFound != value)
                {
                    carFound = value;
                    OnPropertyChanged("CarFound");
                }
            }
        }

        public int Ticks
        {
            get { return ticks; }
            set
            {
                if (ticks != value)
                {
                    ticks = value;
                    OnPropertyChanged("Ticks");
                }
            }
        }

        public int InitialPosition
        {
            get { return initialPosition; }
            set
            {
                if (initialPosition != value)
                {
                    initialPosition = value;
                    OnPropertyChanged("InitialPosition");
                }
            }
        }

        public int Velocity
        {
            get { return velocity; }
            set
            {
                if(velocity != value)
                {
                    velocity = value;
                    OnPropertyChanged("Velocity");
                }
            }
        }
        #endregion

        #region protected members
        protected virtual bool Predicted(int currentPosition)
        {
            if ((maxVelocity - minVelocity) > 1)
                return VelocityPredicted(currentPosition);
            else
                return InitialPositionPredicted(currentPosition);
        }

        protected abstract bool VelocityPredicted(int currentPosition);
        protected abstract bool InitialPositionPredicted(int currentPosition);
        #endregion
    }
}