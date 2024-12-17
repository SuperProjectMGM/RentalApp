using System.Configuration;

namespace wypozyczalnia.Server.Interfaces;

public interface IStorageInterface
{
    public  Task<Uri> GetUriToStorage();
}