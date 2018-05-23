﻿namespace VRTK.Core.Data.Type
{
    using UnityEngine;
    using System;

    /// <summary>
    /// Holds <see cref="Transform"/> information with the ability to override properties without affecting the scene <see cref="Transform"/>.
    /// </summary>
    [Serializable]
    public class TransformData
    {
        /// <summary>
        /// A reference to the original <see cref="Transform"/>.
        /// </summary>
        public Transform transform;
        /// <summary>
        /// Position override of the <see cref="Transform"/> object.
        /// </summary>
        public Vector3? positionOverride = null;
        /// <summary>
        /// Rotation override of the <see cref="Transform"/> object.
        /// </summary>
        public Quaternion? rotationOverride = null;
        /// <summary>
        /// Scale override of the <see cref="Transform"/> object.
        /// </summary>
        public Vector3? localScaleOverride = null;

        /// <summary>
        /// The position of the <see cref="Transform"/> or the <see cref="positionOverride"/> if it is set.
        /// </summary>
        public Vector3 Position => positionOverride ?? transform.position;

        /// <summary>
        /// The rotation of the <see cref="Transform"/> or the <see cref="rotationOverride"/> if it is set.
        /// </summary>
        public Quaternion Rotation => rotationOverride ?? transform.rotation;

        /// <summary>
        /// The localScale of the <see cref="Transform"/> or the <see cref="localScaleOverride"/> if it is set.
        /// </summary>
        public Vector3 LocalScale => localScaleOverride ?? transform.localScale;

        /// <summary>
        /// The state of whether the <see cref="TransformData"/> is valid.
        /// </summary>
        public bool Valid
        {
            get
            {
                return (transform != null);
            }
        }

        /// <summary>
        /// Creates a new <see cref="TransformData"/> for an empty <see cref="Transform"/>.
        /// </summary>
        public TransformData()
        {
        }

        /// <summary>
        /// Creates a new <see cref="TransformData"/> from an existing <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/> to create the <see cref="TransformData"/> from.</param>
        public TransformData(Transform transform)
        {
            this.transform = transform;
        }
    }
}