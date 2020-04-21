namespace MyForumApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.Configuration;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using MyForumApp.Data;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;
    using MyForumApp.Data.Repositories;
    using MyForumApp.Services.Data;
    using MyForumApp.Services.Mapping;
    using MyForumApp.Web.ViewModels.Categories;
    using MyForumApp.Web.ViewModels.Home;
    using MyForumApp.Web.ViewModels.Posts;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public void GetByNameShouldReturnCorrectCategory()
        {
            var repository = new Mock<IDeletableEntityRepository<Category>>();
            repository.Setup(r => r.All()).Returns(new List<Category>
                                                        {
                                                            new Category() { Id = 1, Name = "a", },
                                                            new Category() { Id = 2 },
                                                            new Category() { Id = 3 },
                                                        }.AsQueryable());
            var service = new CategoriesService(repository.Object);
            Assert.Equal("a", service.GetByNameNonGeneric("a").Name);
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetAllShouldReturnAllCategoriesUsingDbContext()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, Category>();
            });

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Category { Id = 1, Name = "donger", Title = "test1", Description = "a" }).GetAwaiter().GetResult();
            repository.AddAsync(new Category { Id = 2, Title = "test2", Description = "b" }).GetAwaiter().GetResult();
            repository.AddAsync(new Category { Id = 3, Title = "test3", Description = "c" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var categoriesService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestCategory).Assembly);
            var categories = categoriesService.GetAll(3);
            Assert.Equal(3, categories.Count());
        }

        public class MyTestCategory : IMapFrom<Category>, IMapTo<Category>
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }
        }
    }
}
