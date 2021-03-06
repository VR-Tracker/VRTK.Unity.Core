﻿using VRTK.Core.Data.Operation;

namespace Test.VRTK.Core.Data.Operation
{
    using UnityEngine;
    using NUnit.Framework;
    using Test.VRTK.Core.Utility.Mock;

    public class TransformPositionExtractorTest
    {
        private GameObject containingObject;
        private TransformPositionExtractor subject;

        [SetUp]
        public void SetUp()
        {
            containingObject = new GameObject();
            subject = containingObject.AddComponent<TransformPositionExtractor>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(subject);
            Object.DestroyImmediate(containingObject);
        }

        [Test]
        public void Extract()
        {
            UnityEventListenerMock extractedListenerMock = new UnityEventListenerMock();
            subject.Extracted.AddListener(extractedListenerMock.Listen);
            subject.source = containingObject;

            Vector3 result = subject.Extract();

            Assert.AreEqual(Vector3.zero, result);
            Assert.AreEqual(Vector3.zero, subject.LastExtractedValue);
            Assert.IsTrue(extractedListenerMock.Received);

            containingObject.transform.position = Vector3.one;
            extractedListenerMock.Reset();

            Assert.IsFalse(extractedListenerMock.Received);

            result = subject.Extract();

            Assert.AreEqual(Vector3.one, result);
            Assert.AreEqual(Vector3.one, subject.LastExtractedValue);
            Assert.IsTrue(extractedListenerMock.Received);
        }

        [Test]
        public void Process()
        {
            UnityEventListenerMock extractedListenerMock = new UnityEventListenerMock();
            subject.Extracted.AddListener(extractedListenerMock.Listen);
            subject.source = containingObject;

            subject.Process();

            Assert.AreEqual(Vector3.zero, subject.LastExtractedValue);
            Assert.IsTrue(extractedListenerMock.Received);

            containingObject.transform.position = Vector3.one;
            extractedListenerMock.Reset();

            Assert.IsFalse(extractedListenerMock.Received);

            subject.Process();

            Assert.AreEqual(Vector3.one, subject.LastExtractedValue);
            Assert.IsTrue(extractedListenerMock.Received);
        }

        [Test]
        public void ExtractInactiveGameObject()
        {
            UnityEventListenerMock extractedListenerMock = new UnityEventListenerMock();
            subject.Extracted.AddListener(extractedListenerMock.Listen);
            subject.source = containingObject;
            subject.gameObject.SetActive(false);

            Vector3 result = subject.Extract();

            Assert.AreEqual(Vector3.zero, result);
            Assert.AreEqual(Vector3.zero, subject.LastExtractedValue);
            Assert.IsFalse(extractedListenerMock.Received);

            containingObject.transform.position = Vector3.one;
            extractedListenerMock.Reset();

            Assert.IsFalse(extractedListenerMock.Received);

            result = subject.Extract();

            Assert.AreEqual(Vector3.zero, result);
            Assert.AreEqual(Vector3.zero, subject.LastExtractedValue);
            Assert.IsFalse(extractedListenerMock.Received);
        }

        [Test]
        public void ExtractInactiveComponent()
        {
            UnityEventListenerMock extractedListenerMock = new UnityEventListenerMock();
            subject.Extracted.AddListener(extractedListenerMock.Listen);
            subject.source = containingObject;
            subject.enabled = false;

            Vector3 result = subject.Extract();

            Assert.AreEqual(Vector3.zero, result);
            Assert.AreEqual(Vector3.zero, subject.LastExtractedValue);
            Assert.IsFalse(extractedListenerMock.Received);

            containingObject.transform.position = Vector3.one;
            extractedListenerMock.Reset();

            Assert.IsFalse(extractedListenerMock.Received);

            result = subject.Extract();

            Assert.AreEqual(Vector3.zero, result);
            Assert.AreEqual(Vector3.zero, subject.LastExtractedValue);
            Assert.IsFalse(extractedListenerMock.Received);
        }
    }
}