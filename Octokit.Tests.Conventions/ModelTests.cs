﻿using System;
using System.Diagnostics;
using System.Linq;
using Octokit.Tests.Helpers;
using Xunit;
using System.Collections.Generic;

namespace Octokit.Tests.Conventions
{
    public class ModelTests
    {
        [Theory]
        [MemberData("ModelTypes")]
        public void HasDebuggerDisplayAttribute(Type modelType)
        {
            AssertEx.HasAttribute<DebuggerDisplayAttribute>(modelType);
        }

        [Theory]
        [MemberData("ResponseModelTypes")]
        public void HasGetterOnlyProperties(Type modelType)
        {
            foreach (var property in modelType.GetProperties())
            {
                var setter = property.GetSetMethod(nonPublic: true);

                Assert.True(setter == null || !setter.IsPublic);
            }
        }

        public static IEnumerable<object[]> ModelTypes
        {
            get { return GetModelTypes(includeRequestModels: true).Select(type => new[] { type }); }
        }

        public static IEnumerable<object[]> ResponseModelTypes
        {
            get { return GetModelTypes(includeRequestModels: false).Select(type => new[] { type }); }
        }

        private static IEnumerable<Type> GetModelTypes(bool includeRequestModels)
        {
            var allModelTypes = new HashSet<Type>();

            var clientInterfaces = typeof(IEventsClient).Assembly.ExportedTypes
                .Where(type => type.IsClientInterface());

            foreach (var exportedType in clientInterfaces)
            {
                var methods = exportedType.GetMethods();

                var modelTypes = methods.Select(method => UnwrapGenericArgument(method.ReturnType));

                if (includeRequestModels)
                {
                    var requestModels = methods.SelectMany(method => method.GetParameters(),
                        (method, parameter) => parameter.ParameterType);

                    modelTypes = modelTypes.Union(requestModels);
                }

                foreach (var modelType in modelTypes.Where(type => type.IsModel()))
                {
                    allModelTypes.Add(modelType);
                }
            }

            return allModelTypes;
        }

        private static Type UnwrapGenericArgument(Type returnType)
        {
            if (returnType.IsGenericType)
            {
                var argument = returnType.GetGenericArgument();
                if (argument.IsModel())
                {
                    return argument;
                }

                return UnwrapGenericArgument(argument);
            }

            return returnType;
        }
    }
}