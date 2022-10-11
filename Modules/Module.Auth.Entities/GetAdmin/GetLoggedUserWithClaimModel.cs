using System.Collections.Generic;
using BaseEntities.Concrete;

namespace Module.Auth.Entities.GetAdmin
{
    public class GetLoggedUserWithClaimModel : GetModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LoginName { get; set; }

        public List<GetOperationClaimModel> UserClaims { get; set; }
    }
}