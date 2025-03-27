namespace APP.Entity.DTOs.Response
{
    public class OverviewResponse
    {
        public int TotalUsers { get; set; }
        public int TotalBookings { get; set; }
        public int TotalBlogs { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
