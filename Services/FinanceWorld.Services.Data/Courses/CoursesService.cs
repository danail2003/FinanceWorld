namespace FinanceWorld.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Services.Mapping;

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

        public async Task DeleteAsync(int id)
        {
            Course course = this.coursesRepository.All().FirstOrDefault(x => x.Id == id);

            this.coursesRepository.Delete(course);

            await this.coursesRepository.SaveChangesAsync();
        }

        public async Task Enroll(ApplicationUser user, int id)
        {
            Course course = this.coursesRepository.All().FirstOrDefault(x => x.Id == id);
            course.Users.Add(user);
            user.Courses.Add(course);

            await this.coursesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.coursesRepository.AllAsNoTracking().To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.coursesRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public async Task<Course> UpdateAsync(int id, EditCoursesDto dto)
        {
            Course course = this.coursesRepository.All().FirstOrDefault(x => x.Id == id);

            course.Name = dto.Name;
            course.Description = dto.Description;
            course.Price = dto.Price;

            await this.coursesRepository.SaveChangesAsync();

            return course;
        }
    }
}
