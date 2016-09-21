using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainCore;
using Repositories.Interfaces;

namespace DomainEF.Interfaces
{
    interface IClientProfileRepository : IEntityRepository<ClientProfile>
    {
    }
}
