using KTCSharpApi.Contexts;
using KTCSharpApi.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace KTCSharpApi.Repositories.Implementations
{
    public class CadPessoaRepository : MongoDBContext<Models.CadPessoa>, ICadPessoaRepository
    {
        public CadPessoaRepository()
        {
        }

        public async Task<List<Models.CadPessoa>> GetBySearchAsync(string value, CancellationToken cancellationToken)
        {
            return await Query
                .Where(w => w.Nome.StartsWith(value))
                .ToListAsync(cancellationToken);
        }
    }
}
