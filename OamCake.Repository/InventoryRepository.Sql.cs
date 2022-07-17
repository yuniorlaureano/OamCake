namespace OamCake.Repository
{
    public partial class InventoryRepository
    {
        private string GetStockSql(string where) => @$"
        SELECT 
			 Pro.[Id]
			,Pro.[Code]
			,Pro.[Name]
			,Pro.[Description]
			,SUM(IIF([Quantity] < 0, [Quantity], 0)) As OutputQty
			,SUM(IIF([Quantity] > 0, [Quantity], 0)) As InputQty
			,SUM(IIF([Quantity] > 0, [Quantity], 0)) + SUM(IIF([Quantity] < 0, [Quantity], 0)) As InStock
		FROM [Product] Pro
			LEFT JOIN [Inventory] Inv on Inv.ProductId = Pro.Id
		{where}
		GROUP BY 
			 Pro.[Id]
			,Pro.[Code]
			,Pro.[Name]
			,Pro.[Description]
        ";

		private string GetInventoryMovementSql(string where, int? page, int? count)
		{
			string paging = page == null ? "" : $" ORDER BY Inv.[Id] OFFSET {page * count} ROWS FETCH NEXT {count} ROWS ONLY";

			return @$"
				SELECT
					 Inv.[Id]
					,Pro.[Id] as ProductId
					,Pro.Code as ProductCode
					,Pro.[Name] as ProductName
					,Pro.[Description] as ProductDescription
					,Inv.[Date]
					,Inv.[Quantity]
				FROM [Inventory] Inv INNER JOIN [Product] Pro
					ON Pro.Id = Inv.[ProductId]
				{where}
				{paging}";
		}

		private string GetInventoryMovementCountSql(string where)
		{
			return @$"
				SELECT count(*) as Count
				FROM [Inventory] Inv INNER JOIN [Product] Pro
					ON Pro.Id = Inv.[ProductId]
				{where}";
		}

		private string GetDashboardCountInput(string where)
		{
			return @$"
				SELECT 
					SUM(IIF([Quantity] > 0, [Quantity], 0)) As InputQty
				FROM [Inventory] Inv {where}";
		}

		private string GetDashboardCountOutput(string where)
		{
			return @$"
				SELECT 
					SUM(IIF([Quantity] < 0, [Quantity], 0)) As OutputQty
				FROM [Inventory] Inv {where}";
		}

		private string GetInventoryMovementSummarySql(string where, int? page, int? count)
		{
			string paging = page == null ? "" : $" ORDER BY Pro.[Id] OFFSET {page * count} ROWS FETCH NEXT {count} ROWS ONLY";

			return @$"
				SELECT 
					 Pro.[Id] as ProductId
					,Pro.Code as ProductCode
					,Pro.[Name] as ProductName
					,Pro.[Description] as ProductDescription
					,SUM(IIF([Quantity] < 0, [Quantity], 0)) As OutputQty
					,SUM(IIF([Quantity] > 0, [Quantity], 0)) As InputQty
				FROM [Inventory] Inv INNER JOIN [Product] Pro
					ON Pro.Id = Inv.[ProductId]
				{where}
				GROUP BY 
					 Pro.[Id]
					,Pro.Code
					,Pro.[Name]
					,Pro.[Description]
				{paging}
				";
		}

		private string GetInventoryMovementSummaryCountSql(string where)
		{
			return @$"
				SELECT 
					count(distinct Pro.[Id]) as [Count]
				FROM [Inventory] Inv INNER JOIN [Product] Pro
				ON Pro.Id = Inv.[ProductId]
				{where}
				";
		}
	}
}
