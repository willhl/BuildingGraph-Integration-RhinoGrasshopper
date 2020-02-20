using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using BuildingGraph.Client.Neo4j;


namespace BuildingGraph.Integration.RhinoGrasshopper
{

    public class Neo4jDatabase : GH_Goo<Neo4jClient>
    {

        public Neo4jDatabase() : base()
        {

        }

        public Neo4jDatabase(Neo4jClient driver) : base()
        {
            Value = driver;
        }

        public override bool IsValid => Value != null;

        public override string TypeName => "Neo4jDriver";

        public override string TypeDescription => "Neo4j Driver";


        public override IGH_Goo Duplicate()
        {
            return new Neo4jDatabase(Value);
        }

        public override string ToString()
        {
            return TypeName;
        }

    }
}
