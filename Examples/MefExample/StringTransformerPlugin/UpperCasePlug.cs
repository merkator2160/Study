using System;
using System.ComponentModel.Composition;

namespace StringTransformerPlugin
{
    public class UpperCasePlug
    {
        [Export("StringTransformer")]
        public String UpperCase(String src)
        {
            return src.ToUpperInvariant();
        }
    }
}