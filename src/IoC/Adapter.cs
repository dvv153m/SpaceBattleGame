using SpaceBattleGame.Contracts.Common;


namespace IoC
{
    public class Adapter
    {
        public object Instance { get; private set; }

        public Adapter(Type type, IUObject obj)
        {
            AdapterGenerator adapterGenerator = new AdapterGenerator();
            string codeToCompile = adapterGenerator.Generate(type, out string generatedClassName);
            Instance = Compiler.CompileAndCreateAdapter<object>(codeToCompile, generatedClassName, obj);
        }
    }
}
