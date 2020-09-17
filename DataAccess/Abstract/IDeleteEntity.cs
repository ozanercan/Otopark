using System.Collections.Generic;

namespace DataAccess.Abstract
{
    /// <summary>
    /// Bu kod State alanı olan tablolar için yazılmıştır.
    /// MakePassive,MakeActive,ListAll methodları vardır.
    /// </summary>
    /// <typeparam name="IEntity">Bir tablo Entitysi</typeparam>
    public interface IDeleteEntity<IEntity> where IEntity : class, new()
    {
        /// <summary>
        /// IsDeleted alanı 0 olanlar yani silinmemiş olanları listeler.
        /// </summary>
        /// <returns></returns>
        List<IEntity> ListByNonDeleted();

        /// <summary>
        /// IsDelete alanını değiştirmek için kullanılır.
        /// false = 0: Silinmedi
        /// true = 1: Silindi olarak kullanılır..
        /// </summary>
        void ChangeIsDeleteField(int id, bool isDeleted);
    }
}