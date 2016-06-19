using System;
using System.Reflection;

namespace CIEDigitalLib.Interfaces
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}