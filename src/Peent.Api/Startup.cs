using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Peent.Api.Infrastructure;
using Peent.Api.Infrastructure.ModelBinders;
using Peent.Application;
using Peent.Application.Categories.Queries.GetCategory;
using Peent.Application.Common.Behaviors;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.ExistsValidator;
using Peent.Application.Common.Validators.UniqueValidator;
using Peent.Domain.Entities;
using Peent.Persistence;

namespace Peent.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddControllers(options =>
                {
                    var workerProvider = options.ModelBinderProviders.First(p => p.GetType() == typeof(ComplexTypeModelBinderProvider));
                    var workerProviderIndex = options.ModelBinderProviders.IndexOf(workerProvider);
                    var paginationBinderProvider = new PaginationModelBinderProvider(workerProvider);
                    var sortsBinderProvider = new SortsModelBinderProvider(paginationBinderProvider);
                    var filtersBinderProvider = new FiltersModelBinderProvider(sortsBinderProvider);
                    options.ModelBinderProviders.Insert(workerProviderIndex, paginationBinderProvider);
                    options.ModelBinderProviders.Insert(workerProviderIndex, sortsBinderProvider);
                    options.ModelBinderProviders.Insert(workerProviderIndex, filtersBinderProvider);

                    options.ModelMetadataDetailsProviders.Insert(0, new DateTimeBinderProvider());
                })
                .AddHybridModelBinder()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetCategoryQueryValidator>())
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
            services.AddHttpContextAccessor();
            var fakeCurrentContextService = new FakeCurrentContextService(Guid.Parse("E78EECD3-87CA-4DBA-B5B2-861BC5A65F4A"),1);
            services.AddSingleton<ICurrentContextService>(provider => fakeCurrentContextService);
//            services.AddScoped<ICurrentContextService, FakeCurrentContextService>();
//            services.AddScoped<IUserAccessor, UserAccessor>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Peent API", Version = "V1" });
            });

            services.AddMediatR(typeof(GetCategoryQueryHandler));
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(TransactionBehavior<,>));
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>));
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(PerformanceBehaviour<,>));
            services.AddScoped(typeof(IExistsInCurrentContextValidator<>), typeof(ExistsInCurrentContextValidator<>));
            services.AddScoped<IExistsInCurrentContextValidatorProvider, ExistsInCurrentContextValidatorProvider>();
            services.AddScoped<IUniqueInCurrentContextValidatorProvider, UniqueInCurrentContextValidatorProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "post API V1");
            });
        }
    }
}
