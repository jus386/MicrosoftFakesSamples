using System;
using System.ComponentModel;

namespace WpfClock
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        private double _hourAngle;
        private double _minutesAngle;
        private double _secondsAngle;
        private long _secondsCount;

        private System.Windows.Threading.DispatcherTimer _timer; 

        public event PropertyChangedEventHandler PropertyChanged;

        public ClockViewModel()
        {
            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;

            DateTime now = DateTime.Now;
            _secondsCount = now.Hour * 3600 + now.Minute * 60 + now.Second;

            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            var hours = _secondsCount / 3600;
            var remainder = _secondsCount - hours * 3600;
            var minutes = remainder / 60;
            remainder = remainder - minutes * 60;
            var seconds = remainder;


            SecondsAngle = seconds * 6;
            MinutesAngle = minutes * 6;
            HourAngle = (hours * 30) + (minutes * 0.5);

            _secondsCount++;
        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public double HourAngle
        {
            get { return _hourAngle; }
            set
            {
                _hourAngle = value;
                NotifyPropertyChanged("HourAngle");
            }
        }

        public double MinutesAngle
        {
            get { return _minutesAngle; }
            set
            {
                _minutesAngle = value;
                NotifyPropertyChanged("MinutesAngle");
            }
        }

        public double SecondsAngle
        {
            get { return _secondsAngle; }
            set
            {
                _secondsAngle = value;
                NotifyPropertyChanged("SecondsAngle");
            }
        }

    }
}
