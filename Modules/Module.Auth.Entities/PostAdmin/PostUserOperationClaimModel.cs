using System;
using System.Collections.Generic;
using BaseEntities.Abstract;
using BaseEntities.Concrete;
using Core.WebHelper;

namespace Module.Auth.Entities.PostAdmin
{
    public class PostUserOperationClaimModel : IUserAssignableModel
    {
        public List<FormItemStatusModel> OperationClaimIds { get; set; }

        public string UserLoginName { get; set; }
        public Guid UserId { get; set; }
    }
}