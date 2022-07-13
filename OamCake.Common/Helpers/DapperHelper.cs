using Dapper;

namespace OamCake.Common.Helpers
{
    public interface IDapperHelper
    {
        Task<IEnumerable<T>> GetAllAsync<T>(string sql, object param);
        Task<T> GetByIdAsync<T>(string sql, object param);
        Task<int> ExecuteAsync(string sql, object param);
        Task ExecuteAsync(string sql, List<object> param);
        Task<int> CountAsync(string sql, object param);
    }

    public class DapperHelper : IDapperHelper
    {
        public readonly IConnection _connection;

        public DapperHelper(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string sql, object param)
        {
            using var connection = _connection.GetConnection();
            return await connection.QueryAsync<T>(sql, param);
        }

        public async Task<T> GetByIdAsync<T>(string sql, object param)
        {
            using var connection = _connection.GetConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }

        public async Task<int> ExecuteAsync(string sql, object param)
        {
            using var connection = _connection.GetConnection();
            return await connection.ExecuteAsync(sql, param);
        }

        public async Task ExecuteAsync(string sql, List<object> param)
        {
            List<Task<int>> list = new List<Task<int>>();
            using var connection = _connection.GetConnection();
            list.AddRange(param.Select(p => connection.ExecuteAsync(sql, p)));
            await Task.WhenAll(list);
        }

        public async Task<int> CountAsync(string sql, object param)
        {
            using var connection = _connection.GetConnection();
            return await connection.ExecuteScalarAsync<int>(sql, param);
        }
    }
}
