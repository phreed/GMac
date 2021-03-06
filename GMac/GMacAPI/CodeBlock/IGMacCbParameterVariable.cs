﻿using GMac.GMacAST.Expressions;

namespace GMac.GMacAPI.CodeBlock
{
    public interface IGMacCbParameterVariable : IGMacCbVariable
    {
        /// <summary>
        /// The data-store value access (of primitive type) associated with this macro parameter variable
        /// </summary>
        AstDatastoreValueAccess ValueAccess { get; }

        /// <summary>
        /// The name of the data-store value access (of primitive type) associated with this macro parameter 
        /// variable
        /// </summary>
        string ValueAccessName { get; }
    }
}