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
        bool _wasCommitted = false;
        public Neo4jConnect() : base("Connect DB", "Connect", "Connects to Neo4j database", "Neo4j", "Nodes")
        {
        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("_neo4jHost", "Host", "DB Host name or IP", GH_ParamAccess.item);
            pManager.AddTextParameter("_neo4jPort", "Port", "DB Port", GH_ParamAccess.item, "6987");
            pManager.AddTextParameter("_neo4jUserName", "Host", "DB Host name or IP", GH_ParamAccess.item);
            pManager.AddTextParameter("_neo4jPassword", "Host", "DB Host name or IP", GH_ParamAccess.item);
            pManager.AddBooleanParameter("_Commit", "Commit", "Commit changes to database", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("_neo4jDriver", "DB", "Neo4j Database connection", GH_ParamAccess.item);

        }

        protected override void SolveInstance(IGH_DataAccess dataAccess)
        {
     

            bool commitChanges = false;
            dataAccess.GetData<bool>("_Commit", ref commitChanges);
            if (_neo4jDB != null && !_wasCommitted && commitChanges)
            {              
                _neo4jDB.Value.CommitAsync().RunSynchronously();
                _wasCommitted = !commitChanges;
                AddRuntimeMessage(GH_RuntimeMessageLevel.Remark, "Changes committed");
                return;
            }


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

}
