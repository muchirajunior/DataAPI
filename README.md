# DATA API

### database
- implimenting db relationships in C#. All the Models in the Models folder and the Database Context class in root folder.
- one to one Relationship `User` and `Business`
- one to many Relationship `Business` and `Product`
- many to many Relationship `Order` and `Product`. using the joint table `OrderProduct`.

- the configuration in the Database context class to create unique field in user class and create many to many join 
```C#
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        //creates a unique field username for user model class i.e user table
        modelBuilder.Entity<User>().HasIndex(user => user.Username).IsUnique(); 
        
        //creates a many to many relationship between Orders and Products  class using OrderProduct as joint table
        modelBuilder.Entity<Order>() .HasMany(order=> order.Products).WithMany(product=>product.Orders).UsingEntity<OrderProduct>();
    }
```
- the `JsonIgnore` attribute used on the `User` model to hide the password field from APIs

### services
- send user an email using SMTP in the `UserService` class.
- creating a JWT token with user roles in the `UserService`. these are used to authorize users as they access different APIs
- To enable nested json in .net adding the following line in program cs as service
```C#
    builder.Services.AddControllers().AddJsonOptions(x =>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
```

### Useful Docs
- HealthChecks [https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks]

### Published
 https://documenter.getpostman.com/view/17338944/2s93sc5Yi1#2e733d89-f0f1-40eb-aa15-d7676e0d19ec