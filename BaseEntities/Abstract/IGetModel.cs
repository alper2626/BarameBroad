using System;

namespace BaseEntities.Abstract
{
    public interface IGetModel
    {
        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }


}