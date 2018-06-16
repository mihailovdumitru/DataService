using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Facade.Implementation;
using Persistance.Facade.Interfaces;
using Persistance.Interfaces;
using Persistance.Mapper;
using Persistance.Repositories;

namespace DataService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCors(o => o.AddPolicy("UrlPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("UrlPolicy"));
            });

            var config = new AutoMapper.MapperConfiguration(mapperConfig => mapperConfig.AddProfile(new MappingProfile()));
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IAnswerRespository, AnswerRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ILectureRepository, LectureRepository>();
            services.AddScoped<IQuestionAnswerRespository, QuestionAnswerRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<ITeacherFacade, TeacherFacade>();
            services.AddScoped<ITeacherLecturesRepository, TeacherLecturesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            //app.UseCors("AllowSpecificOrigin");
            app.UseCors("UrlPolicy");
        }
    }
}