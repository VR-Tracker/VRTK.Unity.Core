namespace VRTK.Prefabs.CameraRigSwitcher
{
    using UnityEngine;
    using UnityEngine.XR;

    public class XRSettingsToggler : MonoBehaviour
    {
        public virtual void EnableXR()
        {
            XRSettings.enabled = true;
        }

        public virtual void DisableXR()
        {
            XRSettings.enabled = false;
        }

        public virtual void EnableXRPositionTracking()
        {
            InputTracking.disablePositionalTracking = true;
        }

        public virtual void DisableXRPositionTracking()
        {
            InputTracking.disablePositionalTracking = false;
        }
    }
}