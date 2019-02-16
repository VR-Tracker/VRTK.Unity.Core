﻿namespace VRTK.Core.Tracking.Modification
{
    using UnityEngine;
    using System.Collections.Generic;

    /// <summary>
    /// Sets the state of the current target to the specified active state.
    /// </summary>
    public class GameObjectStateSwitcher : MonoBehaviour
    {
        /// <summary>
        /// A collection of targets to set the state on when it is the active index.
        /// </summary>
        [Tooltip("A collection of targets to set the state on when it is the active index.")]
        public List<GameObject> targets = new List<GameObject>();
        /// <summary>
        /// The state to set the active index target. All other targets will be set to the opposite state.
        /// </summary>
        [Tooltip("The state to set the active index target. All other targets will be set to the opposite state.")]
        public bool targetState = true;
        /// <summary>
        /// Determines if to execute a switch when the component is enabled.
        /// </summary>
        [Tooltip("Determines if to execute a switch when the component is enabled.")]
        public bool switchOnEnable = true;
        /// <summary>
        /// The index in the collection to start at.
        /// </summary>
        [Tooltip("The index in the collection to start at.")]
        public int startIndex = 0;

        /// <summary>
        /// The current active index in the targets collection.
        /// </summary>
        public int activeIndex = 0;

        /// <summary>
        /// Switches to the next target in the collection and sets to the appropriate state.
        /// </summary>
        public virtual void SwitchNext()
        {
            if (!isActiveAndEnabled)
            {
                return;
            }

            activeIndex++;
            if (activeIndex >= targets.Count)
            {
                activeIndex = 0;
            }

            Switch();
        }

        /// <summary>
        /// Switches to the previous target in the collection and sets to the appropriate state.
        /// </summary>
        public virtual void SwitchPrevious()
        {
            if (!isActiveAndEnabled)
            {
                return;
            }

            activeIndex--;
            if (activeIndex < 0)
            {
                activeIndex = targets.Count - 1;
            }

            Switch();
        }

        /// <summary>
        /// Switches to the a specific target in the collection and sets to the appropriate state.
        /// </summary>
        /// <param name="index">The index of the collection to switch to.</param>
        public virtual void SwitchTo(int index)
        {
            if (!isActiveAndEnabled)
            {
                return;
            }

            activeIndex = Mathf.Clamp(index, 0, targets.Count - 1);
            Switch();
        }

        protected virtual void OnEnable()
        {
            activeIndex = startIndex;
            if (switchOnEnable)
            {
                SwitchTo(startIndex);
            }
        }

        /// <summary>
        /// Switches the current active target state.
        /// </summary>
        protected virtual void Switch()
        {
            if (!isActiveAndEnabled)
            {
                return;
            }

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].SetActive((i == activeIndex ? targetState : !targetState));
            }
        }
    }
}