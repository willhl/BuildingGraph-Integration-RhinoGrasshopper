using System;

using Grasshopper.Kernel;


namespace BuildingGraph.Integration.RhinoGrasshopper
{
    public class CreateNode : GH_Component
    {
        public CreateNode() : base("Create Node", "Create", "Creates a neo4j db node", "Neo4j", "Nodes")
        {
        }

        public override Guid ComponentGuid => new Guid("5B4A7EA1-CD31-43F9-861B-B207016677BA");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("_neo4jDriver", "DB", "Neo4j Database connection", GH_ParamAccess.item);
            pManager.AddGenericParameter("_parameters", "Perams", "List of parameters", GH_ParamAccess.list);
            pManager.AddTextParameter("Node Name", "N", "Node name", GH_ParamAccess.item);
            pManager.AddTextParameter("Node Id", "Id", "Node ID", GH_ParamAccess.item);
            pManager.AddTextParameter("Variable Names", "Vn", "Variable Names", GH_ParamAccess.list);
            pManager.AddGenericParameter("Variable Values", "Vv", "Variable Values", GH_ParamAccess.list);
        }


        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {

            pManager.AddGenericParameter("_neo4jPendingNode", "DB", "Neo4j Database connection", GH_ParamAccess.item);

        }

        protected override void SolveInstance(IGH_DataAccess dataAccess)
        {
            Neo4jDatabase neo4jDB = null;

            if (!dataAccess.GetData("_neo4jDriver", ref neo4jDB))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "DB connection required");
                return;
            }


           //neo4jDB.Value.Push();

        }
    }

}
