public class SalesReport
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalSales { get; set; }
    public int TotalOrders { get; set; }
    public decimal AverageOrderValue { get; set; }
    public List<ProductSalesSummary> TopSellingProducts { get; set; }
    public List<CustomerSalesSummary> TopCustomers { get; set; }
} 