﻿using System;
using Microsoft.JSInterop;

namespace Toolbelt.Blazor
{
    /// <summary>
    /// Implementation of loading bar UI.
    /// </summary>
    public class LoadingBar : IDisposable
    {
        private readonly HttpClientInterceptor Interceptor;

        private readonly IJSRuntime JSRuntime;

        /// <summary>
        /// Initialize LoadingBar service instance.
        /// </summary>
        public LoadingBar(HttpClientInterceptor interceptor, IJSRuntime jSRuntime)
        {
            this.Interceptor = interceptor;
            this.JSRuntime = jSRuntime;
            interceptor.BeforeSend += Interceptor_BeforeSend;
            interceptor.AfterSend += Interceptor_AfterSend;
        }

        private void Interceptor_BeforeSend(object sender, EventArgs e) => BeginLoading();

        private void Interceptor_AfterSend(object sender, EventArgs e) => EndLoading();

        internal void ConstructDOM()
        {
            JSRuntime?.InvokeAsync<object>("eval", "Toolbelt.Blazor.loadingBar.constructDOM()");
        }

        /// <summary>
        /// Begin loading bar UI.
        /// </summary>
        public void BeginLoading()
        {
            JSRuntime?.InvokeAsync<object>("eval", "Toolbelt.Blazor.loadingBar.beginLoading()");
        }

        /// <summary>
        /// End loading bar UI.
        /// </summary>
        public void EndLoading()
        {
            JSRuntime?.InvokeAsync<object>("eval", "Toolbelt.Blazor.loadingBar.endLoading()");
        }

        /// <summary>
        /// Unsubscribe events that are provided from HttpClientInterceptor.
        /// </summary>
        public void Dispose()
        {
            Interceptor.BeforeSend -= Interceptor_BeforeSend;
            Interceptor.AfterSend -= Interceptor_AfterSend;
        }
    }
}
