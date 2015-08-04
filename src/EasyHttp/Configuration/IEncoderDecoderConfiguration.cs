namespace EasyHttp.Configuration
{
    using EasyHttp.Codecs;

    public interface IEncoderDecoderConfiguration
    {
        IEncoder GetEncoder();
        IDecoder GetDecoder();
    }
}