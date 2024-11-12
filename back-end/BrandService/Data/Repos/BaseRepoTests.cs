using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandService.Data.Repos
{
    public class BaseRepoTests
    {
        private readonly TestBaseRepo<TestEntity> _repository;
        private readonly FakeDataContext _context;

        public BaseRepoTests()
        {
            // Gera um banco de dados com um nome único para cada teste
            var options = new DbContextOptionsBuilder<FakeDataContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;

            _context = new FakeDataContext(options);
            _repository = new TestBaseRepo<TestEntity>(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            var entity = new TestEntity { Id = 1, Name = "Entity1" };
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            Assert.Contains(entity, await _context.TestEntities.ToListAsync());
        }

        [Fact]
        public async Task ListAsync_ShouldReturnAllEntities()
        {
            var entity1 = new TestEntity { Id = 1, Name = "Entity1" };
            var entity2 = new TestEntity { Id = 2, Name = "Entity2" };

            await _repository.AddAsync(entity1);
            await _repository.AddAsync(entity2);
            await _context.SaveChangesAsync();

            var result = await _repository.ListAsync();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Remove_ShouldRemoveEntity()
        {
            var entity = new TestEntity { Id = 1, Name = "Entity1" };
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            _repository.Remove(entity);
            await _context.SaveChangesAsync();

            Assert.DoesNotContain(entity, await _context.TestEntities.ToListAsync());
        }

        [Fact]
        public async Task FindAsync_ShouldReturnEntity_WhenExists()
        {
            var entity = new TestEntity { Id = 1, Name = "Entity1" };
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            var foundEntity = await _repository.FindAsync(1);
            Assert.NotNull(foundEntity);
            Assert.Equal(entity.Name, foundEntity?.Name);
        }

        [Fact]
        public async Task FindAsync_ShouldReturnNull_WhenEntityNotFound()
        {
            var entity = await _repository.FindAsync(999); // Non-existent ID
            Assert.Null(entity);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity_WhenExists()
        {
            var entity = new TestEntity { Id = 1, Name = "Entity1" };
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            entity.Name = "UpdatedEntity";
            _repository.Update(entity);
            await _context.SaveChangesAsync();

            var updatedEntity = await _repository.FindAsync(1);
            Assert.NotNull(updatedEntity);
            Assert.Equal("UpdatedEntity", updatedEntity?.Name);
        }


        [Fact]
        public async Task AddAsync_ShouldNotAddDuplicateEntity()
        {
            var entity = new TestEntity { Id = 1, Name = "Entity1" };
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            // Attempt to add a duplicate entity
            await _repository.AddAsync(entity);
            var count = await _context.TestEntities.CountAsync();

            Assert.Equal(1, count); // Ensure that only one instance is added
        }


        [Fact]
        public async Task ListAsync_ShouldReturnEmpty_WhenNoEntitiesAdded()
        {
            var result = await _repository.ListAsync();
            Assert.Empty(result); // Ensure that no entities are returned
        }

    }

    // Repositorio genérico de teste
    public class TestBaseRepo<T> where T : class
    {
        protected readonly DbSet<T> dbSet;
        public TestBaseRepo(FakeDataContext context) => dbSet = context.Set<T>();

        public virtual async Task AddAsync(T entity) => await dbSet.AddAsync(entity);
        public virtual async Task<T?> FindAsync(long id) => await dbSet.FindAsync(id);
        public virtual async Task<IList<T>> ListAsync() => await dbSet.ToListAsync();
        public virtual void Remove(T entity) => dbSet.Remove(entity);
        public virtual void Update(T entity) => dbSet.Update(entity);
    }

    // Classe do contexto de teste
    public class FakeDataContext : DbContext
    {
        public DbSet<TestEntity> TestEntities => Set<TestEntity>();

        public FakeDataContext(DbContextOptions<FakeDataContext> options) : base(options) { }
    }

    // Entidade de teste
    public class TestEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
