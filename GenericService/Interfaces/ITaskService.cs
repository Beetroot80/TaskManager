using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceEntities;

namespace GenericService
{
    interface ITaskService: IService<ServiceTask>
    {
        ServiceTask GetById(int id);
    }
}
