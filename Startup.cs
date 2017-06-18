using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Logging;

namespace distweb
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole();
      loggerFactory.AddDebug();

      var rewriteOptions = new RewriteOptions();

      if (env.IsProduction())
      {
        rewriteOptions.AddRedirectToHttps();
      }

      rewriteOptions.AddRewrite(@"^(?:(?!.*\.ico$|assets|bundle).)*$", "index.html", true);

      app
        .UseRewriter(rewriteOptions)
        .UseStaticFiles();
    }
  }
}
