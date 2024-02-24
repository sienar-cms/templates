using Microsoft.Extensions.DependencyInjection;
using Sienar;
using Sienar.Email;
using Sienar.Extensions;
using Sienar.Infrastructure.Plugins;
using Sienar.Infrastructure;
using Sienar.Infrastructure.Menus;
using SienarWeb;
using SienarWeb.Data;

await SienarAppBuilder
	.Create(args, typeof(Program).Assembly)
	.AddRootDbContext<AppDbContext>(o => o.UseDb())
	.AddPlugin<SienarCmsPlugin>()
	.AddPlugin<MailKitPlugin>()
#if DEBUG
	.AddPlugin<DevmodePlugin>()
#endif
	.ConfigureTheme<CustomTheme>()
	.SetupApp(
		app =>
		{
			app.Services.MigrateDb<AppDbContext>(DataUtils.GetDbPath());
			app
				.ConfigureStyles(p => p.Add("/css/site.css"))
				.ConfigureComponents(
					p => p.DefaultLayout = typeof(SienarWebDefaultLayout))
				.ConfigureMenu(SetupMenu);
		})
	.BuildBlazor()
	.RunAsync();

void SetupMenu(IMenuProvider p)
{
	p
		.Access(SienarWebMenuNames.MainMenu)
		.AddLink(
			new()
			{
				Text = "Home",
				Url = "/"
			})
		.AddLink(
			new()
			{
				Text = "Register",
				Url = DashboardUrls.Account.Register.Index,
				RequireLoggedOut = true
			})
		.AddLink(
			new()
			{
				Text = "Log in",
				Url = DashboardUrls.Account.Login,
				RequireLoggedOut = true
			})
		.AddLink(
			new()
			{
				Text = "Log out",
				Url = DashboardUrls.Account.Logout,
				RequireLoggedIn = true
			});
}