using System;

namespace Foster.Framework;

/// <summary>
/// An Implementation of the System Module that supports the OpenGL Graphics API
/// </summary>
public interface ISystemOpenAL
{
    /// <summary>
    /// An OpenGL Graphics Context
    /// </summary>
    public abstract class Context : IDisposable
    {
        /// <summary>
        /// Whether the Context is Disposed
        /// </summary>
        public abstract bool IsDisposed { get; }

        /// <summary>
        /// Didposes the Context
        /// </summary>
        public abstract void Dispose();
    }
}
