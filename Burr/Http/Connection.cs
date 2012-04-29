﻿using System;
using System.Threading.Tasks;
using Burr.Helpers;

namespace Burr.Http
{
    public class Connection : IConnection
    {
        static readonly Func<IBuilder, IApplication> defaultStack = builder => { return builder.Run(new HttpClientAdapter()); };

        Uri baseAddress;

        public Connection(Uri baseAddress)
        {
            Ensure.ArgumentNotNull(baseAddress, "baseAddress");

            this.baseAddress = baseAddress;
        }

        IBuilder builder;
        public IBuilder Builder
        {
            get
            {
                return builder ?? (builder = new Builder());
            }
            set
            {
                builder = value;
            }
        }

        public async Task<IResponse<T>> GetAsync<T>(string endpoint)
        {
            return await Run<T>(new Request
            {
                Method = "GET",
                BaseAddress = baseAddress,
                Endpoint = endpoint
            });
        }

        public async Task<IResponse<T>> PatchAsync<T>(string endpoint, object body)
        {
            return await Run<T>(new Request
            {
                Method = "PATCH",
                BaseAddress = baseAddress,
                Endpoint = endpoint,
                Body = body
            });
        }

        async Task<IResponse<T>> Run<T>(IRequest request)
        {
            var env = new Env<T>
            {
                Request = request,
                Response = new Response<T>()
            };

            await App.Call<T>(env);

            return env.Response;
        }

        IApplication app;
        public IApplication App
        {
            get
            {
                return app ?? (app = MiddlewareStack(Builder));
            }
        }

        Func<IBuilder, IApplication> middlewareStack;
        public Func<IBuilder, IApplication> MiddlewareStack
        {
            get
            {
                return middlewareStack ?? (middlewareStack = defaultStack);
            }
            set
            {
                middlewareStack = value;
            }
        }
    }
}
