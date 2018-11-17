﻿using VRTK.Core.Data.Type.Transformation;

namespace Test.VRTK.Core.Data.Type.Transformation
{
    using UnityEngine;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Test.VRTK.Core.Utility.Mock;

    public class Vector3SubtractorTest
    {
        private GameObject containingObject;
        private Vector3Subtractor subject;

        [SetUp]
        public void SetUp()
        {
            containingObject = new GameObject();
            subject = containingObject.AddComponent<Vector3Subtractor>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(subject);
            Object.DestroyImmediate(containingObject);
        }

        [Test]
        public void Transform()
        {
            UnityEventListenerMock transformedListenerMock = new UnityEventListenerMock();
            subject.Transformed.AddListener(transformedListenerMock.Listen);
            subject.collection = new List<Vector3>(new Vector3[3]);

            Assert.AreEqual(Vector3.zero, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);

            subject.SetElement(0, Vector3.one * 3f);
            subject.SetElement(1, Vector3.one);
            subject.CurrentIndex = 2;
            Vector3 result = subject.Transform(Vector3.one);

            Assert.AreEqual(Vector3.one, result);
            Assert.AreEqual(Vector3.one, subject.Result);
            Assert.IsTrue(transformedListenerMock.Received);
        }

        [Test]
        public void TransformWithIndex()
        {
            UnityEventListenerMock transformedListenerMock = new UnityEventListenerMock();
            subject.Transformed.AddListener(transformedListenerMock.Listen);
            subject.collection = new List<Vector3>(new Vector3[3]);

            Assert.AreEqual(Vector3.zero, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);

            subject.SetElement(0, Vector3.one * 3f);
            subject.SetElement(1, Vector3.one);
            Vector3 result = subject.Transform(2, Vector3.one);

            Assert.AreEqual(Vector3.one, result);
            Assert.AreEqual(Vector3.one, subject.Result);
            Assert.IsTrue(transformedListenerMock.Received);
        }

        [Test]
        public void TransformExceedingIndex()
        {
            UnityEventListenerMock transformedListenerMock = new UnityEventListenerMock();
            subject.Transformed.AddListener(transformedListenerMock.Listen);
            subject.collection = new List<Vector3>(new Vector3[2]);

            Assert.AreEqual(Vector3.zero, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);

            // adds (3f,3f,3f) to index 0 -> adds (1f,1f,1f) to index 1 -> attempts to add (2f,2f,2f) to index 2 but is out of range so sets it at index 1
            // collection result is [(3f,3f,3f), (2f,2f,2f)]

            subject.SetElement(0, Vector3.one * 3f);
            subject.SetElement(1, Vector3.one);
            Vector3 result = subject.Transform(2, Vector3.one * 2f);

            Assert.AreEqual(Vector3.one, result);
            Assert.AreEqual(Vector3.one, subject.Result);
            Assert.IsTrue(transformedListenerMock.Received);
        }

        [Test]
        public void TransformInactiveGameObject()
        {
            UnityEventListenerMock transformedListenerMock = new UnityEventListenerMock();
            subject.Transformed.AddListener(transformedListenerMock.Listen);
            subject.collection = new List<Vector3>(new Vector3[2]);
            subject.gameObject.SetActive(false);

            Assert.AreEqual(Vector3.zero, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);

            subject.SetElement(0, Vector3.one * 3f);
            Vector3 result = subject.Transform(1, Vector3.one);

            Assert.AreEqual(Vector3.zero, result);
            Assert.AreEqual(Vector3.zero, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);
        }

        [Test]
        public void TransformInactiveComponent()
        {
            UnityEventListenerMock transformedListenerMock = new UnityEventListenerMock();
            subject.Transformed.AddListener(transformedListenerMock.Listen);
            subject.collection = new List<Vector3>(new Vector3[2]);
            subject.enabled = false;

            Assert.AreEqual(Vector3.zero, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);

            subject.SetElement(0, Vector3.one * 3f);
            Vector3 result = subject.Transform(1, Vector3.one);

            Assert.AreEqual(Vector3.zero, result);
            Assert.AreEqual(Vector3.zero, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);
        }
    }
}
