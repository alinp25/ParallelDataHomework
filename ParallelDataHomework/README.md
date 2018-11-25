# Microsoft Student Partners - Homework on Parallel C#

Implement a database management system that supports instructions for Select, Where, Update, Insert, Delete, and Create tables.

All instructions except creation should be parallel.

The data types supported by columns are string, bool, datetime, double.

Classical operators (+, -, ÷, *) must be able to be used on all classical data types.

Restrictions:
- static extension methods only
- use only LINQ and Parallel.For / Foreach (without classical forks, whiles and so on)
- No string format, just string interpolation