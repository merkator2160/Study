using System;
using System.ComponentModel.Composition;
using System.Text;

namespace StringTransformerPlugin
{
    public class AnagramPlug
    {
        [Export("StringTransformer")]
        public String MakeAnagram(String src)
        {
            var result = new StringBuilder();
            for (var i = src.Length - 1; i >= 0; --i)
                result.Append(src[i]);

            return result.ToString();
        }
    }
}