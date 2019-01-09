namespace VRTK.Core.Prefabs.CameraRig.UnityXRCameraRig.Input
{
    using UnityEngine;
    using VRTK.Core.Action;

    /// <summary>
    /// Listens for the specified key state and emits the appropriate action.
    /// </summary>
    public class VRTrackerButtonAction : BooleanAction
    {
        public enum ButtonType {Trigger, Grip, A, B, X, Y, Joystick};

        public VRTracker.Manager.VRT_Tag.TagType controllerType;
        public ButtonType buttonType;

        private VRTracker.Manager.VRT_Tag tag;


		private void Start()
		{
			
		}

		protected virtual void Update()
        {
         //   Receive(Input.GetKey(keyCode));
        }
    }
}