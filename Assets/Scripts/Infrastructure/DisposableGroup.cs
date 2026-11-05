using System;
using System.Collections.Generic;

namespace Unity.BossRoom.Infrastructure
{
    /// <summary>
    /// A group of disposables that are disposed together, last added first, like nested using blocks.
    /// </summary>
    public class DisposableGroup : IDisposable
    {
        readonly List<IDisposable> m_Disposables = new List<IDisposable>();

        bool m_IsDisposed;

        public void Dispose()
        {
            if (m_IsDisposed)
            {
                return;
            }

            m_IsDisposed = true;

            for (var i = m_Disposables.Count - 1; i >= 0; i--)
            {
                m_Disposables[i].Dispose();
            }

            m_Disposables.Clear();
        }

        /// <summary>
        /// Adds a disposable to the group. Null is ignored; once the group is disposed, the disposable is disposed at once.
        /// </summary>
        public void Add(IDisposable disposable)
        {
            if (disposable == null)
            {
                return;
            }

            if (m_IsDisposed)
            {
                disposable.Dispose();
                return;
            }

            m_Disposables.Add(disposable);
        }
    }
}
