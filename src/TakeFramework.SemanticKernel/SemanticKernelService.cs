using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeFramework.SemanticKernel
{
    public class SemanticKernelService(Kernel kernel)
    {
        private readonly Kernel _kernel = kernel;

        public async Task Test(string prompt)
        {
            var summarize = _kernel.CreateFunctionFromPrompt(prompt, executionSettings: new OpenAIPromptExecutionSettings
            {
                MaxTokens = 100
            });


            string text1 = @"
1st Law of Thermodynamics - Energy cannot be created or destroyed.
2nd Law of Thermodynamics - For a spontaneous process, the entropy of the universe increases.
3rd Law of Thermodynamics - A perfect crystal at zero Kelvin has zero entropy.";

            string text2 = @"
1. An object at rest remains at rest, and an object in motion remains in motion at constant speed and in a straight line unless acted on by an unbalanced force.
2. The acceleration of an object depends on the mass of the object and the amount of force applied.
3. Whenever one object exerts a force on another object, the second object exerts an equal and opposite on the first.";

            Console.WriteLine(await _kernel.InvokeAsync(summarize, new KernelArguments(new PromptExecutionSettings
            {
                ExtensionData = new Dictionary<string, object>
                {
                    //{ "text1",text1 },
                    { "text2",text2 }

                }
            })));

            //Console.WriteLine(await kernel.InvokeAsync(summarize, new KernelArguments(text1)));
            //Console.WriteLine(await _kernel.InvokeAsync(summarize, new KernelArguments(text2)));

        }

    }
}
