using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace J5Bot
{
    public class Startup
    {
        //// Added by me
        //private string _twitchbot_username;
        //private string _twitch_token;
        //private string _twitch_channel;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //if (env.IsDevelopment())
            //{
            //    var builder = new ConfigurationBuilder();
            //    builder.AddUserSecrets<Startup>();
            //    Configuration = builder.Build();
            //}
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //_twitchbot_username = Configuration["AppSettings:twitchbot_username"];
            //_twitch_token = Configuration["AppSettings:twitch_token"];
            //_twitch_channel = Configuration["AppSettings:twitch_channel"];

            //var BotConfig = Configuration.GetSection("AppSettings").Get<BotSettings>();

            //_twitchbot_username = BotConfig.Twitchbot_username;
            //_twitch_token = BotConfig.Twitch_token;
            //_twitch_channel = BotConfig.Twitch_channel;

            //services.Configure<BotSettings>(options => Configuration.GetSection("AppSettings").Bind(options));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
