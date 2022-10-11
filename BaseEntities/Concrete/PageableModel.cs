using System.Collections.Generic;
using BaseEntities.Abstract;

namespace BaseEntities.Concrete
{
    public class PageableModel<T> : IPageableModel<T>
    where T: class,new()
    {
        public List<T> PageItems { get; set; }

        public decimal MaxPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalItems { get; set; }

        public string Query { get; set; }

    }
}