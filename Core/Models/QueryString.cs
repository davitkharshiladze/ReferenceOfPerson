using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReferenceOfPerson.Core.Models
{
    public abstract class QueryString
    {
        private const int MaxPageSize = 50;
        private const int MinPageSize = 5;
        private const int MinPage = 1;

        private int _pageSize = 10;
        private int _page { get; set; } = 1;

        public string SearchTerm { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }

        public int PageSize
        {
            get => _pageSize;
            
            set
            {
                if (value > MinPageSize && value < MaxPageSize )
                    _pageSize = value;

                if (value < MinPageSize)
                    _pageSize = MinPageSize;

                if (value > MaxPageSize)
                    _pageSize = MaxPageSize;

            }
        }

        public int Page
        {
            get => _page;

            set => _page = value < MinPage ? MinPage : value;
        }
    }
}