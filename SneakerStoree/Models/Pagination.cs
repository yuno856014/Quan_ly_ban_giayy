using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Models
{
    public class Pagination
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public string Keyword { get; private set; }
        public Pagination(int totalItems, int? page_number, int? page_size , string keyword)
        {
            TotalItems = totalItems;
            PageSize = page_size.HasValue ? page_size.Value : 10;
            TotalPages = (int)Math.Ceiling((decimal)TotalItems / (decimal)PageSize);
            CurrentPage = page_number.HasValue ? page_number.Value : 1;
            Keyword = keyword;
            StartPage = CurrentPage - 3;
            EndPage = CurrentPage + 1; 
            if(StartPage <= 0)
            {
                EndPage -= (StartPage - 1);
                StartPage = 1;
            }    
            if(EndPage > TotalItems)
            {
                EndPage = TotalPages;
                if(EndPage > 5)
                {
                    StartPage = EndPage - 4;    
                }    
            }    
        }
    }
}
