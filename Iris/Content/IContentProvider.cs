using System.IO;
using System.Threading.Tasks;

namespace Iris.Content
{
    public interface IContentProvider
    {
        string ContentRoot { get; }

        T Load<T>(string relativePath) where T : class;
        byte[] Read(string relativePath);
        Stream GetFileStream(string relativePath);
        
        Task<T> LoadAsync<T>(string relativePath) where T : class;
        Task<byte[]> ReadAsync(string relativePath);
    }
}