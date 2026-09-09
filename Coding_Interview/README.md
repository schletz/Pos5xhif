# Coding interview for C# (junior to intermediate)

## Preparation

Have a look at the following videos on YouTube by Nick Chapsas (a very popular YouTube channel in the field of .NET programming):

- https://www.youtube.com/watch?v=U3QvTaw224o
- https://www.youtube.com/watch?v=Yd4GnWeEkIY

## Refactoring

The *CustomerManager* class implements a small service class for customer management.
It provides registration and maximum amount calculation.
The following points need to be considered when refactoring the class:

- The business logic is correct, the results must be exactly the same after your refactoring.
- Don't change the method signatures of public methods, the main program must work and should produce the same output without modification.

## Theory questions

- Which SOLID principles were violated?
  Explain why.
- What's the problem with using *DateTime.Now*?
- Describe the advantage of using interfaces in your implementation.
- Describe the difference between returning List\<T\>, IReadOnlyList\<T\> or IEnumerable\<T\>.
