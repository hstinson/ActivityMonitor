using System.Threading.Tasks;

namespace ActivityMonitor
{
    /// <summary>
    /// Interface for something that can perform a notification.
    /// </summary>
    internal interface INotifier
    {
        /// <summary>
        /// Initializes the notification system.
        /// </summary>
        /// <returns>Awaitable task</returns>
        Task InitializeAsync();

        /// <summary>
        /// Performs a notification.
        /// </summary>
        /// <returns>Awaitable task</returns>
        Task NotifyAsync();
    }
}