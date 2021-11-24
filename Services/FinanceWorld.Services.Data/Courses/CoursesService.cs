namespace FinanceWorld.Services.Data.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Models;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;

        public CoursesService(IDeletableEntityRepository<Course> coursesRepository)
            => this.coursesRepository = coursesRepository;

        public async Task<int> CreatAsync(CreateCourseDto dto)
        {
            Course course = new()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
            };

            await this.coursesRepository.AddAsync(course);
            await this.coursesRepository.SaveChangesAsync();

            return course.Id;
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>()
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Course> UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
