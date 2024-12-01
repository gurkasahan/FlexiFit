var builder = WebApplication.CreateBuilder(args);


//Group Members:
//Gurkaranjit Asahan 991704979
//Alfed Adenekan 991688457
//Kamal Singh 991690092

//Group Name: Three Headed Goat

//Project Name: FlexiFit

//Description: Gym Membership system where members can sign up, manage their memberships and book classes that are being offered.
//What makes our project unique is that we will offer home workouts within our app.
//It will not only be a gym membership app but also implement workouts within the app.


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
