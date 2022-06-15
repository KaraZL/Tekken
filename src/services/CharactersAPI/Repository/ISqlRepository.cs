using CharactersAPI.Models;

namespace CharactersAPI.Repository
{
    public interface ISqlRepository<T> where T : class
    {
        public Task<bool> Add(T item);
        public Task<bool> Update(T item);
        public Task<bool> Delete(int id);
        public Task<T> Get(int id);
        public Task<IEnumerable<T>> GetAll();
    }
}
