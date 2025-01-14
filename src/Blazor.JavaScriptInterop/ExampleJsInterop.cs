using Microsoft.JSInterop;

namespace Blazor.JavaScriptInterop
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public sealed class ExampleJsInterop : IAsyncDisposable
    {
        private readonly Lazy<ValueTask<IJSObjectReference>> _moduleTask;

        public ExampleJsInterop(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/Blazor.JavaScriptInterop/blazorators.js"));
        }

        public async ValueTask<string> Prompt(string message)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<string>("showPrompt", message);
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}