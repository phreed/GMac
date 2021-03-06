﻿using System.Collections.Generic;
using System.Linq;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Operator;

namespace GMac.GMacAST.Expressions
{
    public sealed class AstMacroCall : AstExpression 
    {
        internal BasicPolyadic AssociatedPolyadicExpression { get; }

        internal override ILanguageExpression AssociatedExpression => AssociatedPolyadicExpression;

        internal ILanguageOperator AssociatedOperator => AssociatedPolyadicExpression.Operator;

        internal GMacMacro AssociatedMacro => AssociatedPolyadicExpression.Operator as GMacMacro;


        public override bool IsValidMacroCall => AssociatedPolyadicExpression != null &&
                                                 AssociatedMacro != null;

        /// <summary>
        /// The macro called in this expression
        /// </summary>
        public AstMacro CalledMacro => IsValidMacroCall ? new AstMacro(AssociatedMacro) : null;

        /// <summary>
        /// The macro parameters datastore value access used in this macro call
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> UsedParameters
        {
            get
            {
                var assignments =
                    AssociatedPolyadicExpression.Operands as OperandsByValueAccess;

                if (ReferenceEquals(assignments, null)) return null;

                return
                    assignments.AssignmentsList.Select(
                        item => new AstDatastoreValueAccess(item.LhsValueAccess)
                        );
            }
        }

        /// <summary>
        /// The macro parameters assignments used in this macro call
        /// </summary>
        public IEnumerable<KeyValuePair<AstDatastoreValueAccess, AstExpression>> Assignments
        {
            get
            {
                var assignments = 
                    AssociatedPolyadicExpression.Operands as OperandsByValueAccess;

                if (ReferenceEquals(assignments, null)) return null;

                return 
                    assignments.AssignmentsList.Select(
                        item => new KeyValuePair<AstDatastoreValueAccess, AstExpression>(
                            new AstDatastoreValueAccess(item.LhsValueAccess),
                            item.RhsExpression.ToAstExpression()
                            )
                        );
            }
        }


        internal AstMacroCall(BasicPolyadic expr)
        {
            AssociatedPolyadicExpression = expr;
        }
    }
}
