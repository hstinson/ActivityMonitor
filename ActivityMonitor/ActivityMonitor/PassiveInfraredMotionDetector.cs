using System;
using Windows.Devices.Gpio;

namespace ActivityMonitor
{
    /// <summary>
    /// Detects motion using a HC-SR501 passive IR sensor.
    /// </summary>
    /// <remarks>
    /// See https://www.mpja.com/download/31227sc.pdf and 
    /// https://electrosome.com/pir-motion-sensor-hc-sr501-raspberry-pi/ for more info on the sensor and using it
    /// with a Raspberry Pi.
    /// </remarks>
    class PassiveInfraredMotionDetector : IMotionDetector
    {
        private readonly int pinNumber;
        private readonly uint timeoutMilliseconds;
        private readonly GpioController gpio;
        private GpioPin inputPin;

        /// <inheritdoc />
        public event Action MotionDetected;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pinNumber">Input pin number where the PIR sensor is connected.</param>
        /// <param name="timeoutMilliseconds">Debounce timeout in milliseconds</param>
        public PassiveInfraredMotionDetector(int pinNumber, uint timeoutMilliseconds)
        {
            this.pinNumber = pinNumber;
            this.timeoutMilliseconds = timeoutMilliseconds;

            gpio = GpioController.GetDefault();
            if (gpio == null)
            {
                throw new Exception("GPIO controller not found.");
            }         
        }

        /// <inheritdoc />
        public void Initialize()
        {
            Cleanup();

            inputPin = gpio.OpenPin(pinNumber);
            inputPin.Write(GpioPinValue.Low);
            inputPin.SetDriveMode(GpioPinDriveMode.Input);
            inputPin.DebounceTimeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
            inputPin.ValueChanged += InputPinValueChanged;
        }

        /// <inheritdoc />
        public void Cleanup()
        {
            if (inputPin != null)
            {
                inputPin.ValueChanged -= InputPinValueChanged;
                inputPin.Dispose();
            }
        }

        private void InputPinValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            // If low to high, motion was detected
            if (args.Edge == GpioPinEdge.RisingEdge)
            {
                OnMotionDetected();
            }
        }

        private void OnMotionDetected()
        {
            MotionDetected?.Invoke();
        }
    }
}
