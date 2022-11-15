using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Contracts.Common;
using System.Reflection;

namespace IoC
{    

    public class AdapterGenerator
    {
        public string Generate(Type type, out string generatedClassName)
        {
            generatedClassName = $"{type.Name.Remove(0, 1)}Adapter";

            var iCommandType = typeof(ICommand);
            var iUObject = typeof(IUObject);

            string adapterSourceCode = $"public class {generatedClassName}: {type.FullName} " +
                                       "{ " +
                                       $"private {iUObject.FullName} _obj; " +
                                       $"public {generatedClassName}({iUObject.FullName} obj) " +
                                       "{ _obj = obj; } ";

            PropertyInfo[] propertyInfo = type.GetProperties();
            foreach (PropertyInfo property in propertyInfo)
            {
                var propertyName = property.Name;
                var propertyTypeName = property.PropertyType.FullName;

                adapterSourceCode += $"public {propertyTypeName} {propertyName} ";
                adapterSourceCode += "{ ";
                adapterSourceCode += $"get => IoC.IoC.Resolve<{propertyTypeName}>(\"{type.Name}.{propertyName}.Get\", _obj); ";
                if(property.SetMethod != null)
                    adapterSourceCode += $"set => IoC.IoC.Resolve<{iCommandType.FullName}>(\"{type.Name}.{propertyName}.Set\", _obj, value).Execute(); ";

                adapterSourceCode += "} ";
            }

            adapterSourceCode += "} ";

            return adapterSourceCode;
        }
    }
}
