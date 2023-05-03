namespace Assets.Scripts.Infrastructure.Services.JSonReader
{
    public interface IJsonReaderService
    {
        T ReadData<T>(string path);
    }
}
