using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingCore.Entites.Base
{
    interface IEntityBase<T>
    {
        T Id { get; }

    }
}
