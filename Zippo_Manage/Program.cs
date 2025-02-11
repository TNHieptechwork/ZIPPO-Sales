namespace Zippo_Manage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromSeconds(30);
                option.Cookie.HttpOnly = true;
                option.Cookie.IsEssential = true;
            });
            builder.Services.AddDistributedMemoryCache();
            var app = builder.Build();

            app.UseSession();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "sanpham",
                pattern: "{controller=SanPham}/{action=Index}/{id?}");
            //app.MapControllerRoute(
            //   name: "dashBoard",
            //   pattern: "{controller=Home}/{action=dashBoardManage}/{id?}");
            app.Run();
        }
    }
}