using System;
using System.ComponentModel;
using System.Timers;
using System.Windows.Input;

namespace CarFinder
{
    public class Car : INotifyPropertyChanged
    {
        #region private fields
        private int initialPosition;
        private int position;
        private int velocity = 0;
        private int ticks;
        private Timer timer;
        private ICommand start;
        private ICommand stop;
        #endregion

        #region constructors
        public Car()
        {
            Random rnd = new Random();
            initialPosition = rnd.Next(-1000, 1000);
            while (velocity == 0)
            {
                velocity = rnd.Next(-1000, 1000); 
            }
            timer = new Timer(1);
            timer.Elapsed += Timer_Elapsed;
        }

        public Car(int initialPosition, int velocity) : this()
        {
            this.initialPosition = initialPosition;
            this.velocity = velocity;
        }
        #endregion

        #region events and handlers
        public event EventHandler moved;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }        

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;

            ticks++;
            Position += velocity;
            moved?.Invoke(this, EventArgs.Empty);

            if (ticks < 5000000)
                timer.Enabled = true;
        }
        #endregion

        #region public members
        public int Position
        {
            get { return position; }
            set
            {
                if(position != value)
                {
                    position = value;
                    OnPropertyChanged("Position");
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

        public int InitialPosition
        {
            get { return initialPosition; }
            set
            {
                if (initialPosition != value)
                {
                    initialPosition = value;
                    OnPropertyChanged("initialPosition");
                }
            }
        }


        public ICommand StartCommand
        {
            get
            {
                if (start == null)
                    start = new CarCommand(Start);
                return start;
            }
        }

        public ICommand StopCommand
        {
            get
            {
                if (stop == null)
                    stop = new CarCommand(Stop);
                return stop;
            }
        }

        public void Start()
        {
            Position = initialPosition;
            ticks = 0;
            timer.Enabled = true;
        }

        public void Stop()
        {
            timer.Enabled = false;
        }
        #endregion
    }
}