using Entities.Abstract;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IDtoMethods<T> where T : class, IDto, new()
    {
        /// <summary>
        /// Warning! This methods in join query for sql
        /// </summary>
        /// <returns></returns>
        List<T> ListView();
    }
}