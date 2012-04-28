﻿using System;
using System.Threading.Tasks;

namespace Burr.Http
{
    public class Connection : IConnection
    {
        Uri baseAddress;

        public Connection(Uri baseAddress)
        {
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

        static readonly Func<IBuilder, IApplication> defaultStack = builder => { return builder.Run(new HttpClientAdapter()); };
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
