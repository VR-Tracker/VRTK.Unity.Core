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
                    Receive(tracker.trigger);

                else if (buttonType == ButtonType.Grab)
                    Receive(tracker.grab);

                else if (buttonType == ButtonType.A)
                    Receive(tracker.a);

                else if (buttonType == ButtonType.B)
                    Receive(tracker.b);

                else if (buttonType == ButtonType.X)
                    Receive(tracker.x);

                else if (buttonType == ButtonType.Y)
                    Receive(tracker.y);

                else if (buttonType == ButtonType.Joystick)
                    Receive(tracker.joystick);
            }
        }
    }
}