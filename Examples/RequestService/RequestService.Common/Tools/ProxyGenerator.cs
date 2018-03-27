using System;
using System.Reflection;
using System.Reflection.Emit;



namespace RequestService.Common.Tools
{
    /// <summary>
    /// Usage example:
    /// 
    ///public interface IDuck
    ///{
    ///    void Swim();
    ///}
    /// 
    ///public class SwimmingAnimal
    ///{
    ///  public void Swim()
    ///  {
    ///    Console.WriteLine("Hey, we are swimming!");
    ///  }
    ///}
    ///SwimmingAnimal animal = new SwimmingAnimal();
    ///IDuck duck = ProxyGenerator.GetProxy<IDuck>(animal);
    ///duck.Swim();
    /// 
    /// </summary>
    public static class ProxyGenerator
    {
        public static T GetProxy<T>(object instance) where T : class
        {
            Type ifaceType = typeof(T);
            AssemblyBuilder asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName("TMP"), AssemblyBuilderAccess.Run);
            ModuleBuilder modBuilder = asmBuilder.DefineDynamicModule("TMP");
            TypeBuilder typeBuilder = modBuilder.DefineType(ifaceType.Name + "_Impl", TypeAttributes.Public);
            typeBuilder.AddInterfaceImplementation(ifaceType);
            FieldBuilder instanceField = typeBuilder.DefineField("instance", typeof(Object), FieldAttributes.Private);
            FieldBuilder instanceTypeField = typeBuilder.DefineField("instanceType", typeof(Type), FieldAttributes.Private);
            ConstructorBuilder conBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(Object), typeof(Type) });
            ILGenerator cgen = conBuilder.GetILGenerator();
            cgen.Emit(OpCodes.Ldarg, 0);
            cgen.Emit(OpCodes.Call, typeof(Object).GetConstructor(new Type[0]));
            cgen.Emit(OpCodes.Ldarg, 0);
            cgen.Emit(OpCodes.Ldarg, 1);
            cgen.Emit(OpCodes.Stfld, instanceField);
            cgen.Emit(OpCodes.Ldarg, 0);
            cgen.Emit(OpCodes.Ldarg, 2);
            cgen.Emit(OpCodes.Stfld, instanceTypeField);
            cgen.Emit(OpCodes.Ret);

            foreach (MethodInfo mi in ifaceType.GetMethods())
            {
                if (!mi.Name.StartsWith("get_") && !mi.Name.StartsWith("set_"))
                    GenerateMethod(typeBuilder, mi, instanceField, instanceTypeField, false);
            }

            foreach (PropertyInfo pi in ifaceType.GetProperties())
            {
                PropertyBuilder propBuilder = typeBuilder.DefineProperty(pi.Name, pi.Attributes,
                    pi.PropertyType, null);

                if (pi.CanRead)
                {
                    MethodInfo mi = ifaceType.GetMethod("get_" + pi.Name);
                    propBuilder.SetGetMethod(GenerateMethod(typeBuilder, mi, instanceField, instanceTypeField, true));
                }

                if (pi.CanWrite)
                {
                    MethodInfo mi = ifaceType.GetMethod("set_" + pi.Name);
                    propBuilder.SetSetMethod(GenerateMethod(typeBuilder, mi, instanceField, instanceTypeField, true));
                }
            }

            Type type = typeBuilder.CreateType();
            return Activator.CreateInstance(type, new object[] { instance, instance.GetType() }) as T;
        }
        private static MethodBuilder GenerateMethod(TypeBuilder typeBuilder, MethodInfo mi, FieldBuilder instanceField, FieldBuilder instanceTypeField, bool specialName)
        {
            ParameterInfo[] pars = mi.GetParameters();
            Type[] parTypes = pars.Length > 0 ? new Type[pars.Length] : null;

            for (int i = 0; i < pars.Length; i++)
                parTypes[i] = pars[i].ParameterType;

            MethodAttributes ma = MethodAttributes.Public | MethodAttributes.Virtual |
                (specialName ? MethodAttributes.HideBySig | MethodAttributes.SpecialName : MethodAttributes.HideBySig);

            MethodBuilder methBuilder = typeBuilder.DefineMethod(mi.Name, ma,
                mi.ReturnType != typeof(void) ? mi.ReturnType : null, parTypes);

            for (int i = 0; i < pars.Length; i++)
                methBuilder.DefineParameter(i + 1, pars[i].Attributes, pars[i].Name);
            ILGenerator gen = methBuilder.GetILGenerator();
            LocalBuilder loc = gen.DeclareLocal(typeof(MethodInfo));
            int offset = 0;

            if (pars.Length > 0)
            {
                gen.DeclareLocal(typeof(object[]));
                gen.DeclareLocal(typeof(object[]));
                offset += 2;
            }

            if (mi.ReturnType != typeof(void))
            {
                gen.DeclareLocal(methBuilder.ReturnType);
                offset++;
            }

            gen.Emit(OpCodes.Ldarg, 0);
            gen.Emit(OpCodes.Ldfld, instanceTypeField);
            gen.Emit(OpCodes.Ldstr, methBuilder.Name);
            gen.EmitCall(OpCodes.Callvirt, typeof(Type).GetMethod("GetMethod", new Type[] { typeof(String) }), new Type[] { typeof(String) });
            gen.Emit(OpCodes.Stloc, 0);

            if (pars.Length > 0)
            {
                gen.Emit(OpCodes.Ldc_I4, pars.Length);
                gen.Emit(OpCodes.Newarr, typeof(Object));
                gen.Emit(OpCodes.Stloc, 2);
                gen.Emit(OpCodes.Ldloc, 2);

                for (int i = 0; i < pars.Length; i++)
                {
                    ParameterInfo p = pars[i];
                    gen.Emit(OpCodes.Ldc_I4, i);
                    gen.Emit(OpCodes.Ldarg, i + 1);

                    if (p.ParameterType.IsValueType)
                        gen.Emit(OpCodes.Box, p.ParameterType);

                    gen.Emit(OpCodes.Stelem_Ref);
                    gen.Emit(OpCodes.Ldloc, 2);
                }

                gen.Emit(OpCodes.Stloc, 1);
                gen.Emit(OpCodes.Ldloc, 0);
                gen.Emit(OpCodes.Ldarg, 0);
                gen.Emit(OpCodes.Ldfld, instanceField);
                gen.Emit(OpCodes.Ldloc, 1);
            }
            else
            {
                gen.Emit(OpCodes.Ldloc, 0);
                gen.Emit(OpCodes.Ldarg, 0);
                gen.Emit(OpCodes.Ldfld, instanceField);
                gen.Emit(OpCodes.Ldnull);
            }

            Type[] methParams = new Type[] { typeof(Object), typeof(Object[]) };
            gen.EmitCall(OpCodes.Callvirt, typeof(MethodBase).GetMethod("Invoke", methParams), methParams);

            if (mi.ReturnType != typeof(void))
            {
                if (methBuilder.ReturnType.IsValueType)
                    gen.Emit(OpCodes.Unbox_Any, methBuilder.ReturnType);

                gen.Emit(OpCodes.Stloc, offset);
                gen.Emit(OpCodes.Ldloc, offset);
            }
            else
                gen.Emit(OpCodes.Pop);

            gen.Emit(OpCodes.Ret);
            return methBuilder;
        }
    }
}
