﻿namespace GMac.GMacCompiler
{
    public static class GMacCompilerOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public enum LowLevelPropagation
        {
            /// <summary>
            /// Only propagate a temp low-level item if it is associated with a constant
            /// </summary>
            PropagateConstant = 1,

            /// <summary>
            /// Only propagate a temp low-level item if it is associated with a constant or a single variable
            /// </summary>
            PropagateSingleVariable = 2,

            /// <summary>
            /// Only propagate a temp low-level item if it is associated with a constant or an expression that depends on a single variable
            /// </summary>
            PropagateSingleVariableDependent = 3
        }

        /// <summary>
        /// If true, any metric products on non-orthogonal derived frames' multivectors are replaced by equivalent
        /// orthogonal operations using derived-to-base and base-to-derived outermorphisms on the derived frames
        /// When this flag is used better compilation time is achieved but more computations are generated.
        /// 
        /// This flag is used during AST basic expressions generation.
        /// </summary>
        public static bool ForceOrthogonalMetricProducts { get; set; }

        /// <summary>
        /// When True the low-level optimizer attempts to extract all common sub-expressions in all rhs values
        /// and refactor the sub-expressions as temporary variables. This may take longer time during 
        /// low level optimization.
        /// 
        /// This flag is used during low-level optimization of a macro's code
        /// </summary>
        public static bool ReduceLowLevelRhsSubExpressions { get; set; }

        /// <summary>
        /// When True the low-level generator uses Mathematica's Simplify[] function on all rhs values before assigning
        /// them to lhs temp or output variables.
        /// 
        /// This flag us used during low-level generation of a macro's code
        /// </summary>
        public static bool SimplifyLowLevelRhsValues { get; set; }

        /// <summary>
        /// During low-level intermediate code generation this option selects a method for using the item in the
        /// RHS of any following items. If item A depende on item B in its RHS value (for example 'A = 3 * B') there are 3 cases:
        /// 
        /// 1- Propagate constants only: If B is assigned a constant value (for example 'B = 5') then propagate the value 
        /// of B not the symbol B (A = 15) hence B is not required anymore. Else propagate the symbol B ('A = 3 * B' as is)
        /// 
        /// 2- Propagate constants and single variables: If B is a constant or is assigned a single variable (for example 'B = C')
        /// propagate the RHS assigned to B (i.e. A = 3 * C) hence B is not required anymore. Else propagate the symbol B ('A = 3 * B' as is)
        /// 
        /// 3- Propagate constants and expressions depending on a single variable: If B is a constant or is assigned 
        /// an expression depending on a single variable (for example 'B = C + Power[C, 2]') propagate the RHS assigned to B 
        /// (i.e. A = 3 * C + 3 * Power[C, 2]) hence B is not required anymore. Else propagate the symbol B ('A = 3 * B' as is)
        /// </summary>
        public static LowLevelPropagation LowLevelPropagationMethod { get; set; }


        static GMacCompilerOptions()
        {
            ForceOrthogonalMetricProducts = true;
            ReduceLowLevelRhsSubExpressions = true;
            SimplifyLowLevelRhsValues = true;
            LowLevelPropagationMethod = LowLevelPropagation.PropagateSingleVariableDependent;
        }
    }
}
