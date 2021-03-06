﻿using VRTK.Core.Tracking.Velocity;

namespace Test.VRTK.Core.Tracking.Velocity
{
    using UnityEngine;
    using NUnit.Framework;
    using Test.VRTK.Core.Utility.Mock;


    public class VelocityEmitterTest
    {
        private GameObject containingObject;
        private VelocityEmitter subject;

        [SetUp]
        public void SetUp()
        {
            containingObject = new GameObject();
            subject = containingObject.AddComponent<VelocityEmitter>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(subject);
            Object.DestroyImmediate(containingObject);
        }

        [Test]
        public void EmitAll()
        {
            UnityEventListenerMock velocityEmittedMock = new UnityEventListenerMock();
            UnityEventListenerMock angularVelocityEmittedMock = new UnityEventListenerMock();

            subject.VelocityEmitted.AddListener(velocityEmittedMock.Listen);
            subject.AngularVelocityEmitted.AddListener(angularVelocityEmittedMock.Listen);

            VelocityTrackerMock tracker = VelocityTrackerMock.Generate(true, Vector3.one, Vector3.one);
            subject.source = tracker;

            subject.EmitAll();

            Assert.IsTrue(velocityEmittedMock.Received);
            Assert.IsTrue(angularVelocityEmittedMock.Received);

            Object.DestroyImmediate(tracker.gameObject);
        }

        [Test]
        public void EmitAllInactiveGameObject()
        {
            UnityEventListenerMock velocityEmittedMock = new UnityEventListenerMock();
            UnityEventListenerMock angularVelocityEmittedMock = new UnityEventListenerMock();

            subject.VelocityEmitted.AddListener(velocityEmittedMock.Listen);
            subject.AngularVelocityEmitted.AddListener(angularVelocityEmittedMock.Listen);

            VelocityTrackerMock tracker = VelocityTrackerMock.Generate(true, Vector3.one, Vector3.one);
            subject.source = tracker;
            subject.gameObject.SetActive(false);

            subject.EmitAll();

            Assert.IsFalse(velocityEmittedMock.Received);
            Assert.IsFalse(angularVelocityEmittedMock.Received);

            Object.DestroyImmediate(tracker.gameObject);
        }

        [Test]
        public void EmitAllInactiveComponent()
        {
            UnityEventListenerMock velocityEmittedMock = new UnityEventListenerMock();
            UnityEventListenerMock angularVelocityEmittedMock = new UnityEventListenerMock();

            subject.VelocityEmitted.AddListener(velocityEmittedMock.Listen);
            subject.AngularVelocityEmitted.AddListener(angularVelocityEmittedMock.Listen);

            VelocityTrackerMock tracker = VelocityTrackerMock.Generate(true, Vector3.one, Vector3.one);
            subject.source = tracker;
            subject.enabled = false;

            subject.EmitAll();

            Assert.IsFalse(velocityEmittedMock.Received);
            Assert.IsFalse(angularVelocityEmittedMock.Received);

            Object.DestroyImmediate(tracker.gameObject);
        }
    }
}