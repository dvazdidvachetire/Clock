using Prism.Commands;
using System;
using System.Windows.Threading;

namespace Time.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private static double _ratio = 6;

        private double _secondHandAngle;
        public double SecondHandAngle
        {
            get => _secondHandAngle * _ratio;
            set => SetProperty(ref _secondHandAngle, value);
        }

        private double _minuteHandAngle;
        public double MinuteHandAngle 
        { 
            get => _minuteHandAngle * _ratio; 
            set => SetProperty(ref _minuteHandAngle, value); 
        }

        private double _hourHandAngle;
        public double HourHandAngle 
        { 
            get => _hourHandAngle * 30; 
            set => SetProperty(ref _hourHandAngle, value); 
        }

        private DispatcherTimer _liveTimer;

        public DelegateCommand LoadedEvent => new(TimerStart);
        public DelegateCommand ClosingWindowEvent => new(CloseWindow);

        private void TimerStart()
        {
            _liveTimer = new();
            _liveTimer.Interval = TimeSpan.FromSeconds(1);
            _liveTimer.Tick += (_, _) => 
            { 
                SecondHandAngle = DateTime.Now.Second;
                MinuteHandAngle = DateTime.Now.Minute;
                HourHandAngle = DateTime.Now.Hour;
            };
            _liveTimer.Start();
        }

        private void CloseWindow()
        {
            _liveTimer.Stop();
        }
    }
}
