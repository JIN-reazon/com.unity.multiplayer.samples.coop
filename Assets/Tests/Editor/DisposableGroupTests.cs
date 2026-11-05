using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.BossRoom.Infrastructure;

namespace Unity.BossRoom.Tests.Editor
{
    public class DisposableGroupTests
    {
        class RecordingDisposable : IDisposable
        {
            readonly List<string> m_Log;
            readonly string m_Name;

            public int DisposeCount { get; private set; }

            public RecordingDisposable(List<string> log, string name)
            {
                m_Log = log;
                m_Name = name;
            }

            public void Dispose()
            {
                DisposeCount++;
                m_Log.Add(m_Name);
            }
        }

        [Test]
        public void Add_IgnoresNull()
        {
            var group = new DisposableGroup();

            Assert.DoesNotThrow(() => group.Add(null));
            Assert.DoesNotThrow(() => group.Dispose());
        }

        [Test]
        public void Dispose_DisposesInReverseOrderOfAdd()
        {
            var log = new List<string>();
            var group = new DisposableGroup();
            group.Add(new RecordingDisposable(log, "first"));
            group.Add(new RecordingDisposable(log, "second"));
            group.Add(new RecordingDisposable(log, "third"));

            group.Dispose();

            CollectionAssert.AreEqual(new[] { "third", "second", "first" }, log);
        }

        [Test]
        public void Dispose_Twice_DisposesEachItemOnce()
        {
            var log = new List<string>();
            var item = new RecordingDisposable(log, "item");
            var group = new DisposableGroup();
            group.Add(item);

            group.Dispose();
            group.Dispose();

            Assert.AreEqual(1, item.DisposeCount);
        }

        [Test]
        public void Add_AfterDispose_DisposesTheItemAtOnce()
        {
            var log = new List<string>();
            var item = new RecordingDisposable(log, "late");
            var group = new DisposableGroup();
            group.Dispose();

            group.Add(item);

            Assert.AreEqual(1, item.DisposeCount);
            group.Dispose();
            Assert.AreEqual(1, item.DisposeCount);
        }
    }
}
