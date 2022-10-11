using System;

namespace BaseEntities.Abstract
{
    public interface IPostModel
    {
    }

    public interface ICreateModel : IPostModel
    {
        Guid Id { get; set; }

        DateTime CreateTime { get; }
        
    }

    public interface IUpdateModel : IPostModel
    {
        Guid Id { get; set; }

        DateTime UpdateTime { get; }

        DateTime CreateTime { get; set; }

    }

    public interface IUserAssignableModel : IPostModel
    {
        public string UserLoginName { get;  }
        Guid UserId { get;  }
    }

}