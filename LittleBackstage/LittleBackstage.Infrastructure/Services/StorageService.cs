using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBackstage.Infrastructure.Services
{
    public interface IStorageService : IDependency
    {
        //void Save();
        void Delete(string path);
        IEnumerable<string> GetFiles(string path);
    }

    public class StorageService : IStorageService
    {
        public void Delete(string path)
        {
            File.Delete(path);
        }

        public IEnumerable<string> GetFiles(string path)
        {
            return Directory.GetFiles(path).Select(Path.GetFileName);
        }
    }
}
