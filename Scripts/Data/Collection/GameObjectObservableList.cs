﻿namespace VRTK.Core.Data.Collection
{
    using UnityEngine;
    using UnityEngine.Events;
    using System;

    /// <summary>
    /// Allows observing changes to a <see cref="List{T}"/> of <see cref="GameObject"/>s.
    /// </summary>
    public class GameObjectObservableList : ObservableList<GameObject, GameObjectObservableList.UnityEvent>
    {
        /// <summary>
        /// Defines the event with the <see cref="GameObject"/>.
        /// </summary>
        [Serializable]
        public class UnityEvent : UnityEvent<GameObject>
        {
        }
    }
}