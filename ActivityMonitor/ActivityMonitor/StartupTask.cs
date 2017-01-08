using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace ActivityMonitor
{
    /// <summary>
    /// Entry point into the app.
    /// </summary>
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;
        private IMotionDetector motionDetector;
        private HueLightsNotification lightsNotification;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Defer task instance in order to keep this task running
            deferral = taskInstance.GetDeferral();
            taskInstance.Canceled += TaskInstanceCanceled;
            
            motionDetector = new PassiveInfraredMotionDetector(26, 500);
            motionDetector.Initialize();

            lightsNotification = new HueLightsNotification("[Your app key goes here]");
            lightsNotification.InitializeAsync()
                .Wait();

            motionDetector.MotionDetected += MotionDetectorMotionDetectedAsync;
        }

        private void TaskInstanceCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            motionDetector.Cleanup();
        }

        private async void MotionDetectorMotionDetectedAsync()
        {
            await lightsNotification.NotifyAsync();
        }
    }
}
