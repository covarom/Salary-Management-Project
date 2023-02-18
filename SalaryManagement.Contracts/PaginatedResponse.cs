namespace SalaryManagement.Contracts
{
    public class PaginatedResponse<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemPerPage { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Results { get; set; }
    }

}
