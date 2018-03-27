using System;
using System.ComponentModel.Composition;

namespace StringTransformerPlugin
{
    public class LowerCasePlug
    {
        [Export("StringTransformer")]
        public String LowerCase(String src)
        {
            return src.ToLowerInvariant();
        }
    }
}