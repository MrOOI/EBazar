using System;
using System.Collections.Generic;

namespace EBazar.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Product> Items { get; set; } 
        public Pager Pager { get; set; }

    }

    public class Pager
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems; //barcha bazadagi product
            CurrentPage = currentPage; //hozirgi page
            PageSize = pageSize; //nechtadan taxlash kkligi
            TotalPages = totalPages; //jami sahifalar soni
            StartPage = startPage; //boshlangich page
            EndPage = endPage; //yakuniy page
        }

        public Pager()
        {

        }
       
    }
}
