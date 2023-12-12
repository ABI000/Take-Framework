using TakeFramework.Domain.Entities;
using TakeFramework.Exceptions;

namespace TakeFramework;

public class EntityDictionary
{
    private readonly Dictionary<string, Dictionary<string, string>> entityDic;

    public EntityDictionary()
    {
        var types = DependencyUtil.GetReferencedAssemblies().SelectMany(x => x.GetTypes()).Where(type => !type.IsAbstract && !type.IsInterface && typeof(IEntity).IsAssignableFrom(type));

        entityDic = types.ToDictionary(x => x.Name, x => x.GetProperties().ToDictionary(w => w.Name, w => w.PropertyType.Name!));

    }
    public string GetMemberType(string entityName, string memberName)
    {
        try
        {
            return entityDic[entityName][memberName];
        }
        catch (Exception)
        {
            throw new BusinessException("The input condition was incorrect,Check the interface documentation");
        }
    }
    public object? ChangeType(string entityName, string memberName, object? value)
    {
        var type = GetMemberType(entityName, memberName);
        if (type.Equals("int", StringComparison.CurrentCultureIgnoreCase))
        {
            return ((List<string>)value!).Select(x => int.Parse(x));

        }
        else if (type.Equals("long", StringComparison.CurrentCultureIgnoreCase))
        {
            return ((List<string>)value!).Select(x => long.Parse(x));
        }
        return value;
    }
}
