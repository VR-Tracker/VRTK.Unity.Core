﻿namespace VRTK.Core.Cast
{
    using UnityEngine;
    using UnityEngine.Events;
    using System;
    using System.Collections.Generic;
    using VRTK.Core.Extension;
    using VRTK.Core.Process;
    using VRTK.Core.Rule;

    /// <summary>
    /// The base of casting components that result in points along the cast.
    /// </summary>
    public abstract class PointsCast : MonoBehaviour, IProcessable
    {
        /// <summary>
        /// Holds data about a <see cref="PointsCast"/> event.
        /// </summary>
        [Serializable]
        public class EventData
        {
            /// <summary>
            /// The result of the most recent cast. <see langword="null"/> when the cast didn't hit anything.
            /// </summary>
            public RaycastHit? targetHit;
            /// <summary>
            /// The points along the the most recent cast.
            /// </summary>
            public IReadOnlyList<Vector3> points;

            public EventData Set(EventData source)
            {
                return Set(source.targetHit, source.points);
            }

            public EventData Set(RaycastHit? targetHit, IReadOnlyList<Vector3> points)
            {
                this.targetHit = targetHit;
                this.points = points;
                return this;
            }

            public void Clear()
            {
                Set(default(RaycastHit?), default(IReadOnlyList<Vector3>));
            }
        }

        /// <summary>
        /// Defines the event with the <see cref="EventData"/>.
        /// </summary>
        [Serializable]
        public class UnityEvent : UnityEvent<EventData>
        {
        }

        /// <summary>
        /// The origin point for the cast.
        /// </summary>
        [Tooltip("The origin point for the cast.")]
        public GameObject origin;
        /// <summary>
        /// Allows to optionally affect the cast.
        /// </summary>
        [Tooltip("Allows to optionally affect the cast.")]
        public PhysicsCast physicsCast;
        /// <summary>
        /// Allows to optionally determine targets based on the set rules.
        /// </summary>
        [Tooltip("Allows to optionally determine targets based on the set rules.")]
        public RuleContainer targetValidity;

        /// <summary>
        /// Emitted whenever the cast result changes.
        /// </summary>
        public UnityEvent ResultsChanged = new UnityEvent();

        /// <summary>
        /// The result of the most recent cast. <see langword="null"/> when the cast didn't hit anything or an invalid target according to <see cref="targetValidity"/>.
        /// </summary>
        public RaycastHit? TargetHit
        {
            get
            {
                return targetHit;
            }
            protected set
            {
                targetHit = (value != null && targetValidity.Accepts(value.Value.transform.gameObject) ? value : null);
            }
        }

        /// <summary>
        /// The points along the the most recent cast.
        /// </summary>
        public IReadOnlyList<Vector3> Points => points;

        protected List<Vector3> points = new List<Vector3>();
        protected EventData eventData = new EventData();

        private RaycastHit? targetHit;

        /// <summary>
        /// Casts and creates points along the cast.
        /// </summary>
        public virtual void CastPoints()
        {
            if (!isActiveAndEnabled || origin == null)
            {
                return;
            }
            DoCastPoints();
        }

        /// <inheritdoc />
        public virtual void Process()
        {
            CastPoints();
        }

        /// <summary>
        /// Performs the implementated way of casting points.
        /// </summary>
        protected abstract void DoCastPoints();
    }
}