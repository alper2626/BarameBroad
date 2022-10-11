using System.Collections.Generic;

namespace BaseEntities.Abstract
{
    public interface IPageableModel<T>
        where T : class, new()
    {
        public List<T> PageItems { get; set; }

        public decimal MaxPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalItems { get; set; }

        public string Query { get; set; }

    }
}