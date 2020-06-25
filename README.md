# Fluent Try Catch Finally

## Versions

### 1.0.0

Initial version of the package

### 1.1.0

Added the Static Trier class in order to enable inferred generics. 

### 1.2.0

Added asynchronous support.

## How to use

The following section is all about some usefull information while trying to use this package.

### Instantiate the Trier class

You can create a new instance of the Trier class while specifying the two generic types the class need.
  - T - Defines the object type where the try operation applies.
  - TResult - Defines the type resulting of the try operation.

From there, you can follow the Fluent API exposed by the object, the first method exposed is Try.
  
```csharp
using FluentTryCatch;

var result = new Trier<string, string>(content)
              .Try(someAction)...;
```


### Using the Static Trier class

From version 1.1.0, a static Trier class has been introduced

As opposed when instantiating the Trier class, you don't need to specifiy any generic types.
You'll also need to passe the object you want to work with as a parameter to the Try method.

The static class works as follow. 

```csharp
using FluentTryCatch;

var result = Trier.Try(content, someAction)...;
```
Behind the scenes, the static class instantiate the Trier and call the Try method for you.

```csharp
  public static ICactherOrRethrower<T, TResult> Try<T, TResult>(T content, Func<T, TResult> tryAction)
    => new Trier<T, TResult>(content).Try(tryAction);
```

### Using the Fluent API

While calling the Try method, two possibilities will be offered as how you want to handle your exceptions.

- You can chain multiple catch blocks after the Try statement.
- You can Rethrow the exception automatically.

```csharp
using FluentTryCatch;

//One possiblity is to add one catch block or multiple ones.
var serverResponse = _serverService.Get();

var result = Trier.Try(serverResponse, serverResponse => Map(serverResponse))
                  .Catch<ArgumentNullException>((content, exception) => Console.WriteLine($"An error occured : {exception}"))
                  .Catch<Exception>(content, exception) => Console.WriteLine($"An unexepected error occured : {exception}"))
                  ....;
                  
//Another possiblity is to rethrow automatically the exception when catched
var serverResponse = _serverService.Get();

var result = Trier.Try(serverResponse, serverResponse => Map(serverResponse))
                  .ReThrow()
                  ....;
```

Either one of those method will enable you to add a Finally statement or to Execute the block.

```csharp
using FluentTryCatch;

//Add a finally statement and then execute the block.
var serverResponse = _serverService.Get();

var result = Trier.Try(serverResponse, serverResponse => Map(serverResponse))
                  .Catch<ArgumentNullException>((content, exception) => Console.WriteLine($"An error occured : {exception}"))
                  .Catch<Exception>(content, exception) => Console.WriteLine($"An unexepected error occured : {exception}"))
                  .Finally(x => Console.WriteLine("Cleaning up..."))
                  .Execute();
                  
//Execute directly without specifying a finally block
var serverResponse = _serverService.Get();

var result = Trier.Try(serverResponse, serverResponse => Map(serverResponse))
                  .ReThrow()
                  .Execute();
```

#### Notes

Available as a NuGet package : https://www.nuget.org/packages/TryCatchFinally.Fluent.NetStandard/

##### Licensed under MIT
