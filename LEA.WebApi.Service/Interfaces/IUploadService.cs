using System.IO;

namespace LEA.WebApi.Service.Interfaces
{
    public interface IUploadService
    {
        bool UpdateDatabaseByCSVFile(string fileName);
    }
}
