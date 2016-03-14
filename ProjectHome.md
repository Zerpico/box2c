Box2C is a pure C frontend to Box2D, making it much easier to port across to other languages.

In addition, you can get all of the usage out of your favorite language with the speed and reliability of the original Box2D DLL; no porting required, aside from implementation-specific things.

Box2C was developed originally just for Box2CS, an up-to-date C# frontend to Box2C, but I decided to release both together.

Advantages over code-ports:
  * Native simulation speed
  * Easier to maintain (if a newer version is released that doesn't change the API, we can simply drop in the new .lib and compile)
  * More reliable (no need to worry about improper code porting - implementation is a breeze).

There are a few components to the project:
  * Box2C (the C++ DLL which exports all of the functions required to work from C)
  * Box2CS (C# frontend to Box2C)
  * Testbed (a WinForms/Tao port of the testbed to Box2CS)
  * Box2CSRenderTest (a test I wrote to test features).