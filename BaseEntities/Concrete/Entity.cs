using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseEntities.Abstract;

namespace BaseEntities.Concrete
{
    public class Entity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class LangEntity<T> : Entity
        where T : class, IEntity
    {
        public Guid MasterId { get; set; }

        [ForeignKey("MasterId")]
        public T Master { get; set; }

        public string Lang { get; set; }

    }

    public class CreateEntityModel<TBase, TSub>
        where TBase : class, IEntity, new()
        where TSub : LangEntity<TBase>, new()
    {
        public CreateEntityModel()
        {
            this.Entity.Id = Guid.NewGuid();
            this.LangEntities = new List<TSub>
            {
                new TSub() {Lang="tr-TR",MasterId = this.Entity.Id },
                new TSub() {Lang="en-EN",MasterId = this.Entity.Id }
            };
        }



        public TBase Entity { get; set; }

        public List<TSub> LangEntities { get; set; }
    }

    public class UpdateEntityModel<TBase, TSub>
        where TBase : class, IEntity, new()
        where TSub : LangEntity<TBase>, new()
    {
        public TBase Entity { get; set; }

        public List<TSub> LangEntities { get; set; }
    }

}