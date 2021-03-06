﻿using System.Collections.Generic;
using System.Linq;

namespace TextComposerLib.Text.Parametric
{
    public static class ParametricUtils
    {
        /// <summary>
        /// Generate the given template text from the supplied parameters values
        /// </summary>
        /// <param name="composer"></param>
        /// <param name="paramsValues"></param>
        /// <returns></returns>
        public static string GenerateText(this ParametricComposer composer, params object[] paramsValues)
        {
            composer.SetParametersValues(paramsValues);

            return composer.GenerateText();
        }

        /// <summary>
        /// Generate the given template text from the supplied parameters values
        /// </summary>
        /// <param name="composer"></param>
        /// <param name="paramsValues"></param>
        /// <returns></returns>
        public static string GenerateText(this ParametricComposer composer, params string[] paramsValues)
        {
            composer.SetParametersValues(paramsValues);

            return composer.GenerateText();
        }

        /// <summary>
        /// Generate the given template text from the supplied parameters values
        /// </summary>
        /// <param name="composer"></param>
        /// <param name="paramsValues"></param>
        /// <returns></returns>
        public static string GenerateText(this ParametricComposer composer, IDictionary<string, string> paramsValues)
        {
            composer.SetParametersValues(paramsValues);

            return composer.GenerateText();
        }

        /// <summary>
        /// Generate the given template text from the supplied parameters values
        /// </summary>
        /// <param name="composer"></param>
        /// <param name="paramsValues"></param>
        /// <returns></returns>
        public static string GenerateText(this ParametricComposer composer, IParametericComposerValueSource paramsValues)
        {
            composer.SetParametersValues(paramsValues);

            return composer.GenerateText();
        }


        /// <summary>
        /// Generate text from each template based on the given parameters. This should be used if all templates 
        /// parameters are properly initialized
        /// </summary>
        /// <param name="composer"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GenerateText(this ParametricComposerCollection composer)
        {
            return
                composer
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value.GenerateText()
                    );
        }

        /// <summary>
        /// Apply the given parameters values to all templates in this collection and generate text from
        /// each template based on the given parameters. This should be used if all templates use common
        /// or similar set of parameters
        /// </summary>
        /// <param name="composer"></param>
        /// <param name="paramsValues"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GenerateText(this ParametricComposerCollection composer, params object[] paramsValues)
        {
            return
                composer
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value.GenerateText(paramsValues)
                    );
        }

        /// <summary>
        /// Apply the given parameters values to all templates in this collection and generate text from
        /// each template based on the given parameters. This should be used if all templates use common
        /// or similar set of parameters
        /// </summary>
        /// <param name="composer"></param>
        /// <param name="paramsValues"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GenerateText(this ParametricComposerCollection composer, params string[] paramsValues)
        {
            return
                composer
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value.GenerateText(paramsValues)
                    );
        }

        /// <summary>
        /// Apply the given parameters values to all templates in this collection and generate text from
        /// each template based on the given parameters. This should be used if all templates use common
        /// or similar set of parameters
        /// </summary>
        /// <param name="composer"></param>
        /// <param name="paramsValues"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GenerateText(this ParametricComposerCollection composer, IDictionary<string, string> paramsValues)
        {
            return
                composer
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value.GenerateText(paramsValues)
                    );
        }

        /// <summary>
        /// Apply the given parameters values to all templates in this collection and generate text from
        /// each template based on the given parameters. This should be used if all templates use common
        /// or similar set of parameters
        /// </summary>
        /// <param name="composer"></param>
        /// <param name="paramsValues"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GenerateText(this ParametricComposerCollection composer, IParametericComposerValueSource paramsValues)
        {
            return
                composer
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value.GenerateText(paramsValues)
                    );
        }

        /// <summary>
        /// Apply the given parameters values to all templates in this collection and generate text from
        /// each template based on the given parameters. This should be used if all templates use common
        /// or similar set of parameters at least in their numbers and order
        /// </summary>
        /// <param name="composer"></param>
        /// <param name="paramsValues"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GenerateUsing(this ParametricComposerCollection composer, params object[] paramsValues)
        {
            return
                composer
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value.GenerateUsing(paramsValues)
                    );
        }

        /// <summary>
        /// Apply the given parameters values to all templates in this collection and generate text from
        /// each template based on the given parameters. This should be used if all templates use common
        /// or similar set of parameters at least in their numbers and order
        /// </summary>
        /// <param name="composer"></param>
        /// <param name="paramsValues"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GenerateUsing(this ParametricComposerCollection composer, params string[] paramsValues)
        {
            return
                composer
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value.GenerateUsing(paramsValues)
                    );
        }
    }
}
