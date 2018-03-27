using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MefTest
{
    public class StringTransformer
    {
        public delegate String StringTransformerStrategy(String src);


        public StringTransformer(String assmblyPath)
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(assmblyPath));
            var container = new CompositionContainer(catalog);
            container.SatisfyImportsOnce(this);
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        [ImportMany("StringTransformer")]
        public IEnumerable<StringTransformerStrategy> Transformers { get; set; }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public void Run()
        {
            foreach (var x in Transformers)
            {
                Console.WriteLine(x("Sample StRiNg."));
            }
        }
    }
}