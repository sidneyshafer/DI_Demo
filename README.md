# WazeCredit Project
* [Dependency Injection Overview (.NET 5)](#dependency-injection-overview)
* [WazeCredit DI Specifications](#wazeCredit-dI-specifications)

## Dependency Injection Overview (.NET 5)

ASP.NET Core is designed from scratch to support Dependency Injection.
NET Core injects objects of dependency classes through constructor or method by using built-in inversion of control (IoC) container.
*	Register classes in the IOC container. When a component needs one of these classes, you can directly extract the class from the container rather than creating an individual object in each component. 
*	Classes are created and registered in the container.
Dependency Injection (DI) is a pattern that can help developers decouple the different pieces of their applications.
In ASP.NET Core, both framework services and application services can be injected into your classes rather than being tightly coupled.

DI is an integral part of ASP.NET Core (.NET 5)
Dependency injection is a form of IoC (Inversion of Control)
Dependency injection is the fifth principle of S.O.L.I.D

**Details of ASP.NET Core**
*	Built-in IoC container
*	Built-in container is represented by IServiceProvider
*	Types of service in ASP.NET Core (.NET 5):
  *	Framework Services
  *	Application Services

**Framework Services**
| Service Type | Lifetime	| Description |
| --- | --- | --- |
| IWebHostEnvironment |	Singleton |	Environment, hosting path |
| Microsoft.Extensions.Logging.ILogger<T> |	Singleton |	Logging |
| Microsoft.Extensions.Logging.ILoggerFactory |	Singleton |	Logging |
| Microsoft.AspNetCore.Http.IHttpContextFactory | Transient |	Context with request/response |
| Microsoft.Extensions.Options.IOptions<TOptions> |	Singleton |	Appsettings |
| Microsoft.AspNetCore.Hosting.IStartup |	Singleton |	Startup app configuration |
| Microsoft.AspNetCore.Hosting.Builder.IApplicationBuilderFactory | Tansient |	Appbuilder |

**Service Lifetime**
*	Singleton: same instance for the life of application (unless restarted).
*	Scoped: same instance for one scope (one request in most cases).
*	Transient: different instance every time the service is injected (or every time the service instance is requested).

**Singleton Lifetime Details**
*	Syntax to Register: services.AddSingleton<>
*	Should be used very carefully.
*	Singleton service sends same instance for the life of the application.
*	*E.g. If you click on a view or link on a website, whenever an instance is requested, it will send the same object. It will change only when the application restarts.*

**Scoped Lifetime Details**
*	Syntax to Register: services.AddScoped<>
*	Not ideal for multi-threading.
*	Scoped service sends a new instance for each request.
*	*E.g. If you click on a view or link for a page load, and if that instance is requested 10 times, it will send the same object for all 10 times.*

**Transient Lifetime Details**
*	Syntax to Register: services.AddTransient<>
*	Always try to register a service as transient if unsure.
*	Transient service sends a new instance every time it is requested.
*	*E.g. If you click on a view or link for a page load, and if that instance is requested 10 times, it will send 10 different objects.*

**Ways of Using Dependency Injection in ASP.NET Core**
*	Constructor Injection
*	Action Injection
*	View Injection
*	Middleware Injection

## WazeCredit DI Specifications

**Application Services**
| Service Type | Lifetime	| Description |
| --- | --- | --- |
| IMarketForecaster |	Transient |	MarketResult prediction |
| IValidationChecker |	Scoped |	Error message and validator logic |
| ICreditValidator |	Scoped |	List of Error messages and passed validation logic |
| ICreditApproved |	Scoped |	Credit approved based on model salary |

* Register application services in container in the Startup file.
```
services.AddTransient<IMarketForecaster, MarketForecaster>();
```

**App Settings Service**
*	Register configuration services for app settings in the Startup file.
*	Inject the market forecaster service, as well as the app settings into the Home Controller using IOptions framework service.

**Lifetime Classes & Middleware**
*	Create lifetime classes for transient, scoped, and singleton services.
*	Create custom middleware and inject the lifetime classes, as well as an Http context.
  *	HttpContext is the context for request/response.
*	Register custom middleware in Startup file. 
  *	Middleware is registered the first time an application runs.
  ```
  app.UseMiddleware<CustomMiddleware>();
  ```
*	Register services in container.
*	Create a lifetime controller. Inject the lifetime services into the controller constructor.
*	Create a lifetime view to display the list of lifetime services for a controller and middleware.

**Service Injections**
*	Use view injection in the Home/Index file.
*	Use constructor injection in the Home Controller and Custom Middleware class.
*	Use action injection in the Home Controller for the AllConfigSettings action method, and the Invoke method of the Custom Middleware class.

**Different Ways to Register a Service**
*	Add abstraction and implementation.
  ```
  services.AddTransient<IMarketForecaster, MarketForecaster>();
  ```
*	Concrete interface with implementation that uses the “new” keyword. Only works for singleton.
  ```
  services.AddSingleton<IMarketForecaster>(new MarketForecaster());
  ```
*	If you do not have an abstraction, provide a concrete implementation (singleton uses “new” keyword).
  ```
  services.AddTransient<MarketForecaster>();
  services.AddSingleton<new MarketForecaster>();
  ```

**TryAdd[LIFETIME]<>**
*	Checks to see if an implementation for that service already exists.
*	If the implementation already exists, it will not register the new implementation.
  ```
  services.TryAddTransient<IMarketForecaster, MarketForecaster>();
  ```

**Replace<>**
*	Replaces the previous implementation with new implementation.
*	Requires a service descriptor.
  ```
  services.Replace(ServiceDescriptor.Transient<IMarketForecaster, MarketForecaster>());
  ```

**RemoveAll<>**
*	Removes all implementation services.
*	Requires the service type you want to remove.
  ```
  services.RemoveAll<IMarketForecaster>();
  ```

**Register Multiple Implementations**
*	Register multiple service implementations of the IValidationChecker interface.
  ```
  services.TryAddEnumerable(new[]
  {
      ServiceDescriptor.Scoped<IValidationChecker, AddressValidationChecker>(),
      ServiceDescriptor.Scoped<IValidationChecker, CreditValidationChecker>()
  });
  ```
*	Register the ICreditValidator service. The IValidationChecker is injected into the CreditValidator class as an IEnumerable to get all validations.
  ```
  services.AddScoped<ICreditValidator, CreditValidator>();
  ```

**Conditional Implementation**
*	Register classes to be implemented for ICreditApproved service.
  ```
  services.AddScoped<CreditApprovedHigh>();
  services.AddScoped<CreditApprovedLow>();
  ```
*	Use conditional statements to select which implementation to use for user interface based on credit approved criteria from custom Enum class.
  ```
  services.AddScoped<Func<CreditApprovedEnum, ICreditApproved>>(ServiceProvider => range =>
  {
      switch (range)
      {
          case CreditApprovedEnum.High:
              return ServiceProvider.GetService<CreditApprovedHigh>();
          case CreditApprovedEnum.Low:
              return ServiceProvider.GetService<CreditApprovedLow>();
          default:
              return ServiceProvider.GetService<CreditApprovedLow>();
      }
  });
  ```
*	Inject service into action method of the Home Controller.

**ILogger – Logging to Files**
*	Install NuGet Package: Serilog.Extensions.Logging.File
*	Inject the ILoggerFactory service into the Configure method of the Startup file.
  ```
  public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
  ```
*	Configure middleware for logging to file in Startup.
  ```
  loggerFactory.AddFile(“logs/creditApp-log-{Date}.txt”);
  ```
