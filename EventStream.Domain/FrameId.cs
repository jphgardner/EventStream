namespace EventStream.Domain
{
    public enum FrameId: byte
    {
        Acknowledge,
        Connect,
        Connected,
        Publish,
        Subscribe
    }
}