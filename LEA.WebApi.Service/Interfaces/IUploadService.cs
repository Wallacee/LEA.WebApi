using LEA.WebApi.Service.ViewModel;
using System.Collections.Generic;
using System.IO;

namespace LEA.WebApi.Service.Interfaces
{
    public interface IUploadService
    {
        List<UpdateMatchesReportViewModel> UpdateDatabaseByCSVFile(string fileName);
    }
}
