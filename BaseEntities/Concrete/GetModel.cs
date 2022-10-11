using System;
using BaseEntities.Abstract;

namespace BaseEntities.Concrete
{
    public class GetModel : IGetModel
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class SelectBoxModel
    {
        public Guid Id { get; set; }

        public string DisplayValue { get; set; }
    }

    public class SelectBoxWithExtraModel<T>
    {
        public Guid Id { get; set; }

        public T Extra { get; set; }

        public string DisplayValue { get; set; }
    }

    public class DxLookUpModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

}