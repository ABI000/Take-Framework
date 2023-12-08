using TakeFramework.Domain.Entities;
using TakeFramework.Exceptions;

namespace TakeFramework;

public class EntityDictionary
{
    private static Dictionary<string, Dictionary<string, string>> entityDic;

    public EntityDictionary()
    {
        var types = DependencyUtil.GetReferencedAssemblies().SelectMany(x => x.GetTypes()).Where(type => !type.IsAbstract && !type.IsInterface && typeof(IEntity).IsAssignableFrom(type));

        entityDic = types.ToDictionary(x => x.Name, x => x.GetProperties().ToDictionary(w => w.Name, w => w.PropertyType.Name!));

    }
    // public static EntityDictionary GetInstance()
    // {
    //     if (instance is null)
    //     {
    //         throw new AggregateException("初始化错误");
    //     }
    //     return instance!;
    // }
    public string GetType(string entityName, string memberName)
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
}
