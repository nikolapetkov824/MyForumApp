namespace MyForumApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.Configuration;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query =
                this.categoriesRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        /// <summary>
        /// Method created to mirror generic GetAll
        /// For Unit Testing.
        /// Reason: I have no idea how to test generic methods.
        /// Google was not helpful.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public IEnumerable<Category> GetAll(int? count = null)
        {
            IQueryable<Category> query =
                this.categoriesRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.ToList();
        }


        public T GetByName<T>(string name, int? take = null, int skip = 0)
        {
            var category = this.categoriesRepository
                .All()
                .Where(x => x.Name.Replace("-", " ") == name.Replace("-", " "))
                .To<T>().FirstOrDefault();
            return category;
        }


        /// <summary>
        /// Method created to mirror generic GetByName
        /// For Unit Testing.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public Category GetByNameNonGeneric(string name, int? take = null, int skip = 0)
        {
            var category = this.categoriesRepository
                .All()
                .Where(x => x.Name.Replace("-", " ") == name.Replace("-", " "))
                .FirstOrDefault();
            return category;
        }
    }
}
