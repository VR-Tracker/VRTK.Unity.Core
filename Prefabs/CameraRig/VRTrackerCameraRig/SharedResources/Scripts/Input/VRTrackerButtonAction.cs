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

        public VRTracker.Manager.VRT_Tag.TagType controllerType;
        public ButtonType buttonType;

        private VRTracker.Manager.VRT_Tag tag;


		private void Start()
		{
            tag = VRTracker.Manager.VRT_Manager.Instance.GetTag(controllerType);
		}

		protected virtual void Update()
        {
            if(tag == null)
            {
                tag = VRTracker.Manager.VRT_Manager.Instance.GetTag(controllerType);
                if (tag == null)
                    return; // Could'nt access the controller 
            }

            if(buttonType == ButtonType.Trigger)
                Receive(tag.trigger);

            else if (buttonType == ButtonType.Grab)
                Receive(tag.grab);
        }
    }
}