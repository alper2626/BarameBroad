using System;
using AutoMapper;
using BaseEntities.Concrete;
using BaseEntities.EnumTypes;

namespace Core.LogModule.Entities
{
    public class Log : Entity
    {
        public string UserFullName { get; set; }

        public Guid DataRef { get; set; }

        public string TableName { get; set; }

        public string TableDisplayName { get; set; }

        public string TypeStr { get; set; }

        public string Data { get; set; }

        public OperationType OperationType { get; set; }

        public string Description { get; set; }
    }

}