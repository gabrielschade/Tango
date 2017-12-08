[![Build status](https://ci.appveyor.com/api/projects/status/y516sjww553xnq63?svg=true)](https://ci.appveyor.com/project/gabrielschade/tango)

# Welcome to

![Introdução](https://gabrielschade.gitbooks.io/tango-br/content/assets/logo%20tango.png)

----

In a certain way any C# developer work with some functional concepts like anonymous methods with lambda expressions and high order functions with Linq library, and so on.

C# is an amazing programming language and we as a dev, can improve it a lot more. In this library I implements some of the core functional concepts in order to enhances the experience of developers.

Tango is a set of powerful functional tools for your .NET C# application. With Tango you can write a clean functional code in C#. It can works with pipelines (method and operator), promises, optional and either type values.

Besides that, Tango provides an extension for IEnumerables, Options and Either with the Module namespace. Use popular methods like Map, Map2, Map3, Filter, Reduce, Fold, Scan and so on, always respecting lazy loadness.

Functional programming brings a lot of benefits to your application, and the do you know the best of all? 

It makes development more **fun**!

## How to use
This library is avaiable at [NuGet](https://www.nuget.org/packages/Tango/).

```
Install-Package Tango
```

> *WARNING*
>
> The current version at NuGet is not the lastest version.
> The NuGet version will be updated by the time I finished the documentation.
>

## What's included:

### Types
  - Continuation
  - Optional
  - Either
  - Unit
  
### Modules
  - Collection
  - Option
  - Either

### Functional Concepts
  - Currying
  - Partial Application
  - Convert your Actions to Functions and vice versa.

### Operations
  - Represents the most common operations for int, double, float, string and booleans.
  - An improved way to reduce, fold and scan your collections with these operations.

## Documentation
For a better understanding of the library concepts, methods and objects you can access the documentation:
  - [English version](https://gabrielschade.gitbooks.io/tango/content/) -> in progress...
  - [Portuguese version](https://gabrielschade.gitbooks.io/tango-br/content/) -> in progress...

## Dependecies
This library depends on:
  - [System.ValueTuple](https://www.nuget.org/packages/System.ValueTuple/#)
  - [Microsoft.Net.Compilers](https://www.nuget.org/packages/Microsoft.Net.Compilers/)

Theses dependecies will be installed automatically, but you can also install it manually with NuGet:

> Install-Package System.ValueTuple

> Install-Package Microsoft.Net.Compilers


## License
This library works under [MIT](LICENSE.txt) license.


## From me, the developer

I spent some time to create this library for improving the way of any dev can uses C# and functional programming concepts together.

Functional Programming |> C# :heart:

Follow me on [Twitter](http://www.twitter.com/gabrielschade) and [LinkedIn](https://www.linkedin.com/in/gabrielschade/).

And Star this GitHub repository. :star:

I'm always happy to receive your feedback!
