using System;

namespace ActivityMonitor
{
    /// <summary>
    /// Interface for a senor that can detect motion.
    /// </summary>
    internal interface IMotionDetector
    {
        /// <summary>
        /// Subscribers a notified when motion has been detected.
        /// </summary>
        event Action MotionDetected;

        /// <summary>
        /// Initialize the detector.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Cleans up any resources used by the class.
        /// </summary>
        void Cleanup();
    }
}