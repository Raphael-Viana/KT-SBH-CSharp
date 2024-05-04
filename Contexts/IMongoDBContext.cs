namespace KTCSharpApi.Contexts
{
    public interface IMongoDBContext<TModel>
    {
        Task<TModel> GetAsync(Guid id, CancellationToken cancellationToken);
        Task InsertAsync(TModel model, CancellationToken cancellationToken);
        Task UpdateAsync(TModel model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
