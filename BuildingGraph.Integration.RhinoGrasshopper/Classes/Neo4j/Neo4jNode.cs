using Grasshopper.Kernel.Types;
using BuildingGraph.Client.Model;


namespace BuildingGraph.Integration.RhinoGrasshopper
{
    public class Neo4jNode : GH_Goo<Node>
    {

        public Neo4jNode() : base()
        {

        }

        public Neo4jNode(Node node) : base()
        {
            Value = node;
        }

        public override bool IsValid => Value != null;

        public override string TypeName => "Neo4jNode";

        public override string TypeDescription => "Neo4j Node";


        public override IGH_Goo Duplicate()
        {
            return new Neo4jNode(Value);
        }

        public override string ToString()
        {
            return Value.Label;
        }


    }
}
