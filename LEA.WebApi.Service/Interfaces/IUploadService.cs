using System.IO;

namespace LEA.WebApi.Service.Interfaces
{
    public interface IUploadService
    {
        string UpdateDatabaseByFileCSV(string fileName);
    }
}
