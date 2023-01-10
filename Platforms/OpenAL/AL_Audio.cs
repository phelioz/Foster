using System;
using System.Collections.Generic;
using Foster.Framework;

namespace Foster.OpenAL;

public class AL_Audio : Audio
{
    public override AudioRenderer Renderer => AudioRenderer.OpenAL;

    protected override void ApplicationStarted()
    {
        ApiName = "OpenAL";
    }

    // various resources waiting to be deleted
    internal List<int> BuffersToDelete = new List<int>();
    internal List<int> SourcesToDelete = new List<int>();

    private ALContext context;
    private ALDevice device;

    // stored delegates for deleting graphics resources
    private delegate void DeleteResource(int id);

    protected override void FirstWindowCreated()
    {
        var devices = ALC.GetStringList(GetEnumerationStringList.DeviceSpecifier);

        // Get the default device, then go though all devices and select the AL soft device if it exists.
        string deviceName = ALC.GetString(ALDevice.Null, AlcGetString.DefaultDeviceSpecifier);
        foreach (var d in devices)
        {
            if (d.Contains("OpenAL Soft"))
            {
                deviceName = d;
            }
        }

        device = ALC.OpenDevice(deviceName);
        context = ALC.CreateContext(device, (int[])null);
        ALC.MakeContextCurrent(context);

        ALC.GetInteger(device, AlcGetInteger.MajorVersion, 1, out int alcMajorVersion);
        ALC.GetInteger(device, AlcGetInteger.MinorVersion, 1, out int alcMinorVersion);
        string alcExts = ALC.GetString(device, AlcGetString.Extensions);

        ALC.Attributes.MajorVersion = alcMajorVersion;
        ALC.Attributes.MinorVersion = alcMinorVersion;

        var attrs = ALC.GetContextAttributes(device);

        string exts = AL.Get(ALGetString.Extensions);
        string rend = AL.Get(ALGetString.Renderer);
        string vend = AL.Get(ALGetString.Vendor);
        string vers = AL.Get(ALGetString.Version);

        ApiVersion = new Version(ALC.Attributes.MajorVersion, ALC.Attributes.MinorVersion);
        ApiName = AL.Get(ALGetString.Renderer);
        Frequency = attrs.Frequency ?? 0;
    }


    protected override AudioSource.Platform CreateAudioSource()
    {
        return new AL_AudioSource(this);
    }


    protected override void Shutdown()
    {
        ALC.MakeContextCurrent(ALContext.Null);
        ALC.DestroyContext(context);
        ALC.CloseDevice(device);
    }
}
