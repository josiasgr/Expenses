using Storage;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionServices
    {
        private readonly IStorage _storage;

        public TransactionServices(
            IStorage storage
        )
        {
            _storage = storage;
        }

        public Task<string> Create()
        {
            return Task.FromResult("");
        }
    }
}