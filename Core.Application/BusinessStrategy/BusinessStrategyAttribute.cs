namespace Core.Application.BusinessStrategy;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class BusinessStrategyAttribute(string context) : Attribute
{
    public string Context { get; } = context;
}