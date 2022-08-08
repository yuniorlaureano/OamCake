using OamCake.Common.Helpers;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Repository
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<StockDto>> GetStockAsync(string search);
        Task<TableResponse<MovementDto>> GetInputMovementAsync(string search, int? page, int? count);
        Task<TableResponse<MovementDto>> GetOutputMovementAsync(string search, int? page, int? count);
        Task<int> GetInputMovementDashboardCountAsync();
        Task<int> GetOutputMovementDashboardCountAsync();
        Task<TableResponse<MovementSummaryDto>> GetMovementSummaryAsync(string search, int? page, int? count);
    }

    public partial class InventoryRepository : IInventoryRepository
    {
        private readonly OamCakeContext _db;
        private readonly IDapperHelper _dapperHelper;

        public InventoryRepository(OamCakeContext db, IDapperHelper dapperHelper)
        {
            _db = db;
            _dapperHelper = dapperHelper;
        }
        public async Task<IEnumerable<StockDto>> GetStockAsync(string search)
        {
            var tempSearchTerm = string.IsNullOrWhiteSpace(search) ? "" : " AND Pro.Name like concat('%',@search,'%') ";
            object searchParameters = string.IsNullOrWhiteSpace(search) ? new { } : new { search = search };
            var sql = GetStockSql($"WHERE Pro.DeletedAt is null and Inv.DeletedAt is null {tempSearchTerm}");
            var result = await _dapperHelper.GetAllAsync<StockDto>(sql, searchParameters);
            return result;
        }

        public async Task<TableResponse<MovementDto>> GetInputMovementAsync(string search, int? page, int? count)
        {
            var tempSearchTerm = string.IsNullOrWhiteSpace(search) ? "" : " AND Pro.Name like concat('%',@search,'%') ";
            object searchParameters = string.IsNullOrWhiteSpace(search) ? new { } : new { search = search };
            var selectSql = GetInventoryMovementSql($"WHERE Inv.Quantity > 0 and Pro.DeletedAt is null and Inv.DeletedAt is null {tempSearchTerm}", page, count);
            var sqlCount = GetInventoryMovementCountSql($"WHERE Inv.Quantity > 0 and Pro.DeletedAt is null and Inv.DeletedAt is null {tempSearchTerm}");

            var data = await _dapperHelper.GetAllAsync<MovementDto>(selectSql, searchParameters);
            var rowCount = await _dapperHelper.CountAsync(sqlCount, searchParameters);

            return new TableResponse<MovementDto>()
            {
                Data = data.ToList(),
                Count = rowCount
            };
        }

        public async Task<TableResponse<MovementDto>> GetOutputMovementAsync(string search, int? page, int? count)
        {
            var tempSearchTerm = string.IsNullOrWhiteSpace(search) ? "" : " AND Pro.Name like concat('%',@search,'%') ";
            object searchParameters = string.IsNullOrWhiteSpace(search) ? new { } : new { search = search };

            var selectSql = GetInventoryMovementSql($"WHERE Inv.Quantity < 0 and Pro.DeletedAt is null and Inv.DeletedAt is null {tempSearchTerm}", page, count);
            var countSql = GetInventoryMovementCountSql($"WHERE Inv.Quantity < 0 and Pro.DeletedAt is null and Inv.DeletedAt is null {tempSearchTerm}");

            var data = await _dapperHelper.GetAllAsync<MovementDto>(selectSql, searchParameters);
            var rowCount = await _dapperHelper.CountAsync(countSql, searchParameters);

            return new TableResponse<MovementDto>()
            {
                Data = data.ToList(),
                Count = rowCount
            };
        }

        public async Task<int> GetInputMovementDashboardCountAsync()
        {
            var countSql = GetDashboardCountInput("WHERE inv.DeletedAt is null");
            return await _dapperHelper.CountAsync(countSql, new { });
        }

        public async Task<int> GetOutputMovementDashboardCountAsync()
        {
            var countSql = GetDashboardCountOutput("WHERE inv.DeletedAt is null");
            return await _dapperHelper.CountAsync(countSql, new { });
        }

        public async Task<TableResponse<MovementSummaryDto>> GetMovementSummaryAsync(string search, int? page, int? count)
        {
            var tempSearchTerm = string.IsNullOrWhiteSpace(search) ? "" : " AND Pro.Name like concat('%',@search,'%') ";
            object searchParameters = string.IsNullOrWhiteSpace(search) ? new { } : new { search = search };

            var selectSql = GetInventoryMovementSummarySql($"WHERE Pro.DeletedAt is null and Inv.DeletedAt is null {tempSearchTerm}", page, count);
            var countSql = GetInventoryMovementSummaryCountSql($"WHERE Pro.DeletedAt is null and Inv.DeletedAt is null {tempSearchTerm}");

            var data = await _dapperHelper.GetAllAsync<MovementSummaryDto>(selectSql, searchParameters);
            var rowCount = await _dapperHelper.CountAsync(countSql, searchParameters);

            return new TableResponse<MovementSummaryDto>()
            {
                Data = data.ToList(),
                Count = rowCount
            };
        }
    }
}
