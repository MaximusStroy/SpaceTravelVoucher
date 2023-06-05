using Microsoft.EntityFrameworkCore;
using SpaceTravelVoucher.DataSpaceTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTravelVoucher.DataSpaceTravel.Repository
{
    public class DataRepository<TEntity, TCode> where TEntity : class
    {
        /// <summary>
        /// Удаление объекта из базы данных SpaceTravel
        /// </summary>
        /// <param name="model">Модель которая должна быть удалена</param>
        /// <returns>Возвращает переданную модель если она удалена, null - была ошибка</returns>
        public virtual async Task<TEntity?> Delete(TEntity model) 
        {
            try
            {
                if (model == null) return null;
                using (TESTSPACEContext db = new TESTSPACEContext())
                {
                    db.Set<TEntity>().Remove(model);
                    await db.SaveChangesAsync();
                    return model;
                }

            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// Достать список объектов из базы данных SpaceTravel
        /// </summary>
        /// <returns>Возвращает список</returns>
        public virtual async Task<IEnumerable<TEntity>> GetMany()
        {
            try
            {
                using (TESTSPACEContext db = new TESTSPACEContext())
                {
                    return await db.Set<TEntity>().ToListAsync();
                }
            }
            catch
            {
                return Enumerable.Empty<TEntity>();
            }
        }

        /// <summary>
        /// Достает 1 элемент из базы данных SpaceTravel
        /// </summary>
        /// <param name="item">Свойство модели по которому будет происходить поиск</param>
        /// <returns>Возвращает объект если все успешно, null - была ошибка</returns>
        public virtual async Task<TEntity?> GetOne(TCode item)
        {
            try
            {
                if (item == null) return null;
                using (TESTSPACEContext db = new TESTSPACEContext())
                {
                    return await db.Set<TEntity>().FindAsync(item);
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Вставка в базу данных SpaceTravel
        /// </summary>
        /// <param name="model">Объект для вставки</param>
        /// <returns>Возвращает null, если объект успешно вставлен, иначе ошибка</returns>
        public virtual async Task<string?> Insert(TEntity model)
        {
            try
            {
                if (model == null) return null;
                using (TESTSPACEContext db = new TESTSPACEContext())
                {
                    await db.Set<TEntity>().AddAsync(model);
                    await db.SaveChangesAsync();
                    return null;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Обновление объекта базы данных SpaceTravel
        /// </summary>
        /// <param name="model">Объект который будет обновлен</param>
        /// <returns>Возвращает null, если объект успешно вставлен, иначе ошибка</returns>
        public virtual async Task<string?> Update(TEntity model)
        {
            try
            {
                if (model == null) return null;
                using (TESTSPACEContext db = new TESTSPACEContext())
                {
                    db.Set<TEntity>().Update(model);
                    await db.SaveChangesAsync();
                    return null;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
