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
    public class Neo4jConnect : GH_Component
    {
        public override Guid ComponentGuid => new Guid("A53FD7C8-7424-4629-B5EB-68CADD2AC89C");
        Neo4jDatabase _neo4jDB;

        public Neo4jConnect() : base("Connect DB", "Connect", "Connects to Neo4j database", "Neo4j", "Nodes")
        {
        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("_neo4jHost", "Host", "DB Host name or IP", GH_ParamAccess.item);
            pManager.AddTextParameter("_neo4jPort", "Port", "DB Port", GH_ParamAccess.item, "6987");
            pManager.AddTextParameter("_neo4jUserName", "Host", "DB Host name or IP", GH_ParamAccess.item);
            pManager.AddTextParameter("_neo4jPassword", "Host", "DB Host name or IP", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("_neo4jDriver", "DB", "Neo4j Database connection", GH_ParamAccess.item);

        }

        protected override void SolveInstance(IGH_DataAccess dataAccess)
        {

            string host = string.Empty;
            string userName = string.Empty;
            string password = string.Empty;

            if (!dataAccess.GetData<string>("_neo4jHost", ref host)) AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Host required");
            if (!dataAccess.GetData<string>("_neo4jUserName", ref userName)) AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Username required");
            dataAccess.GetData<string>("_neo4jPassword", ref password);

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(userName))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Missing input data");
                return;
            }

            var driver = new Neo4jClient(new Uri(host), userName, password);
            var vstask = driver.VerifyConnectionAsync();
            vstask.RunSynchronously();

            if (vstask.Exception != null)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Connection failed: " + vstask.Exception.Message);
            }

            if (_neo4jDB == null)
            {
                _neo4jDB = new Neo4jDatabase(driver);
            }
            else
            {
                var oldDb = _neo4jDB.Value;
                oldDb.CloseAsync().ContinueWith((d) => { oldDb.Dispose(); });
                _neo4jDB.Value = driver;
            }

            dataAccess.SetData("_neo4jDriver", _neo4jDB);
        }

    }


    public class CreateNode : GH_Component
    {
        public CreateNode() : base("Create Node", "Create", "Creates a neo4j db node", "Neo4j", "Nodes")
        {
        }

        public override Guid ComponentGuid => new Guid("5B4A7EA1-CD31-43F9-861B-B207016677BA");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("_neo4jDriver", "DB", "Neo4j Database connection", GH_ParamAccess.item);
            pManager.AddGenericParameter("_parameters", "Perams", "Neo4j Database connection", GH_ParamAccess.list);

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
