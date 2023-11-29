namespace API.Helper
{
    public class PaginationHeader
    {
        public PaginationHeader(int cureentPage, int itemsPerPage, int totalItems, int totalPages) 
        {
            CureentPage = cureentPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;   
        }
        public int CureentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}