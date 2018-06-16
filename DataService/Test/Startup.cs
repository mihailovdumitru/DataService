﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Facade.Implementation;
using Persistance.Facade.Interfaces;
using Persistance.Interfaces;
using Persistance.Mapper;
using Persistance.Repositories;

namespace Test
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

            var config = new AutoMapper.MapperConfiguration(mapperConfig => mapperConfig.AddProfile(new MappingProfile()));
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<ITestFacade, TestFacade>();
            services.AddScoped<IAnswerRespository, AnswerRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ILectureRepository, LectureRepository>();
            services.AddScoped<IQuestionAnswerRespository, QuestionAnswerRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("UrlPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}