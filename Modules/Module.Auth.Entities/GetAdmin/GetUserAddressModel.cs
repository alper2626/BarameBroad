using System;
using BaseEntities.Concrete;

namespace Module.Auth.Entities.GetAdmin
{
    public class GetUserAddressModel : GetModel
    {
        public Guid UserId { get; set; }

        public Guid CityId { get; set; }

        public Guid TownId { get; set; }

        public string TownName { get; set; }

        public string CityName { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

    }
}