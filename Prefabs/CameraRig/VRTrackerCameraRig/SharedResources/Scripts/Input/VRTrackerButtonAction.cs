namespace VRTK.Core.Prefabs.CameraRig.UnityXRCameraRig.Input
{
    using UnityEngine;
    using VRTK.Core.Action;

    /// <summary>
    /// Listens for the specified key state and emits the appropriate action.
    /// </summary>
    public class VRTrackerButtonAction : BooleanAction
    {
        public enum ButtonType {Trigger, Grab, A, B, X, Y, Joystick};
        public VRTracker.Manager.VRT_Tag tracker;
        public ButtonType buttonType;

		private void Start()
		{
		}

		protected virtual void Update()
        {
            if (tracker != null)
            {
                if (buttonType == ButtonType.Trigger)
                {
                    Receive(tracker.trigger);
                }

                else if (buttonType == ButtonType.Grab)
                    Receive(tracker.grab);
            }
        }
    }
}