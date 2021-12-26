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
        private readonly IRepository<UserCourse> usersCoursesRepository;

        public CoursesService(IDeletableEntityRepository<Course> coursesRepository, IRepository<UserCourse> usersCoursesRepository)
        {
            this.coursesRepository = coursesRepository;
            this.usersCoursesRepository = usersCoursesRepository;
        }

        public async Task<int> CreatAsync(CourseDto dto)
        {
            Course course = new()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
            };

            foreach (var lesson in dto.Lessons)
            {
                Lesson newLesson = new()
                {
                    Name = lesson.Name,
                    Description = lesson.Description,
                };

                course.Lessons.Add(newLesson);
            }

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
            course.UserCourses.Add(new UserCourse
            {
                AddedByUser = user,
                Course = course,
            });

            await this.coursesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
            => this.coursesRepository.AllAsNoTracking().To<T>().ToList();

        public List<T> GetAllCoursesWithUsers<T>(IEnumerable<int> courses, IEnumerable<string> users)
        {
            var query = this.usersCoursesRepository.All().Where(x => courses.Contains(x.CourseId) && users.Contains(x.AddedByUserId)).To<T>().ToList();

            return query;
        }

        public IEnumerable<int> GetCoursesIds()
            => this.coursesRepository.All().Select(x => x.Id).ToList();

        public T GetById<T>(int id)
            => this.coursesRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();

        public IEnumerable<T> GetMyCourses<T>(string userId)
            => this.coursesRepository.AllAsNoTracking().Where(x => x.UserCourses.Any(u => u.AddedByUser.Id == userId)).To<T>().ToList();

        public IEnumerable<string> GetUsersIds()
            => this.usersCoursesRepository.All().Select(x => x.AddedByUserId).ToList();

        public async Task<Course> UpdateAsync(int id, CourseDto dto)
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
