using System;

namespace Foster.Framework;

/// <summary>
/// The Core Audio Module, used for playing sounds
/// </summary>
public abstract class Audio : AppModule
{
    /// <summary>
    /// The underlying Audio API name
    /// </summary>
    public string ApiName { get; protected set; } = "Unknown";
    /// <summary>
    /// The underlying Audio API version
    /// </summary>
    public Version ApiVersion { get; protected set; } = new Version(0, 0, 0);
    /// <summary>
    /// The underlying Audio API frequency
    /// </summary>
    public int Frequency { get; protected set; } = 0;

    internal readonly AudioSourcePool AudioSourcePool = new AudioSourcePool();

    protected internal abstract AudioSource.Platform CreateAudioSource();

    protected Audio() : base(400) { }

    /// <summary>
    /// The Renderer this Audio Module implements
    /// </summary>
    public abstract AudioRenderer Renderer { get; }

    protected internal override void Startup()
    {
        Log.Info($"{ApiName} {ApiVersion} ({Frequency})");
    }

    protected internal sealed override void Update()
    {
        AudioSourcePool.Update();
    }
}
