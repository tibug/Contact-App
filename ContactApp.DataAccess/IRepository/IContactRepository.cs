using ContactApp.Models;

namespace ContactApp.DataAccess.IRepository;

public interface IContactRepository
{
    Task<(int recordsTotal, int recordsFiltered, IEnumerable<ContactDto> Contacts)> GetContactsAsync(CustomSearchContact request);
    Task<IEnumerable<ExportContactsToCsv>> ExportContactsToCsvAsync(CustomSearchContact searchData, int perExportLimit);
}
