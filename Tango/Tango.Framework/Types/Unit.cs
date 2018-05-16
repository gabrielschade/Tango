namespace Tango.Types
{
    /// <summary>
    /// The unit type is a type that indicates the absence of a specific value; the unit type has only a single value, 
    /// which acts as a placeholder when no other value exists or is needed.
    /// </summary>
    /// <remarks>
    /// The unit type resembles the void type in languages such as C# and C++.
    /// The value of the unit type is often used to hold the place where a value is required by the language syntax, 
    /// but when no value is needed or desired, like in casts from action to functions that needs to return a value.
    /// This is also used as return value in Iterate and Match methods.
    /// </remarks>
    public struct Unit
    { }
}