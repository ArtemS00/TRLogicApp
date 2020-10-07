using System;
using System.Collections.Generic;
using System.Linq;
using TRLogicApp.Entities;

namespace TRLogicApp.Services
{
    public class FakeImageRepository : IRepository<ImageEntity>
    {
        private static Dictionary<int, ImageEntity> _dbSet = new Dictionary<int, ImageEntity>();
        private static int _nextId = 1;

        public IQueryable<ImageEntity> Get()
        {
            return _dbSet.Values.AsQueryable();
        }

        public ImageEntity Get(int id)
        {
            return _dbSet.ContainsKey(id) ?
                _dbSet[id] : null;
        }

        public void Add(ImageEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Must be not null");

            entity.Id = _nextId++;
            _dbSet.Add(entity.Id, entity);
        }

        public void Update(int id, ImageEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Must be not null");
            if (!_dbSet.ContainsKey(id))
                return;
            entity.Id = id;
            _dbSet[id] = entity;
        }

        public void Delete(int id)
        {
            _dbSet.Remove(id);
        }
    }
}
