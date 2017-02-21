using CarFinder;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace CarFinderGame
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ICommand playCommand;
        private ICommand cancelCommand;
        private Car car;
        private IEnumerable<CarFinderBase> carFinders;
        private bool userDefine;
        private int initPosition;
        private int velocity;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public Car Car
        {
            get { return car; }
            set
            {
                if (car != value)
                {
                    car = value;
                    OnPropertyChanged("Car");
                }
            }
        }

        public IEnumerable<CarFinderBase> CarFinders
        {
            get { return carFinders; }
            set
            {
                if (carFinders != value)
                {
                    carFinders = value;
                    OnPropertyChanged("CarFinders");
                }
            }
        }

        public bool UserDefine
        {
            get { return userDefine; }
            set
            {
                if(userDefine != value)
                {
                    userDefine = value;
                    OnPropertyChanged("UserDefine");
                }
            }
        }

        public int InitialPostion
        {
            get { return initPosition; }
            set
            {
                if (initPosition != value)
                {
                    initPosition = value;
                    OnPropertyChanged("InitialPostion");
                }
            }
        }

        public int Velocity
        {
            get { return velocity; }
            set
            {
                if (velocity != value)
                {
                    velocity = value;
                    OnPropertyChanged("Velocity");
                }
            }
        }

        public ICommand PlayCommand
        {
            get
            {
                if (playCommand == null)
                    playCommand = new CarCommand(play);
                return playCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                    cancelCommand = new CarCommand(Cancel);
                return cancelCommand;
            }
        }

        private void Cancel()
        {
            foreach (var finder in CarFinders)
                finder.Cancel();
            car.Stop();
        }

        private void play()
        {
            Car = null;
            if (UserDefine)
                Car = new Car(InitialPostion, Velocity);
            else
                Car = new Car();

            CarFinders = null;
            var finders = new List<CarFinderBase>() { new BinaryCarFinder(car), new GoldenSectionCarFinder(car) };
            CarFinders = finders.AsEnumerable();

            Car.Start();

            foreach (var finder in CarFinders)
                finder.Find();
        }
    }
}
