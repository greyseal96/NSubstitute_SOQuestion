# Test project for demoing a NullReferenceException thrown while debugging and registering multiple return values #

This is a simple demo project to show a NullReferenceException that I'm getting when I try to register multiple return values for a
mock while running in a debugger. [This is a link](https://stackoverflow.com/questions/68808898/why-does-registering-another-return-value-on-an-nsubstitute-mock-with-a-differen) to the related SO question.

To reproduce:

1. Set a breakpoint at the opening brace of the getMockFoo method.
2. Start debugging and step through the code with the debugger.
    1. The first "Returns" registration works fine.
    2. The second "Returns" throws a NRE.
3. Hit continue to continue debugging.  The test should finish successfully.
4. Try other arrangements.
    1. Try commenting out the first "Returns" registration.  The second registration should now work without throwing an NRE.
    2. Try rearranging the "Returns" registrations, i.e. put the second one first.  No matter which one comes second, the second
       registration will throw a NRE.
