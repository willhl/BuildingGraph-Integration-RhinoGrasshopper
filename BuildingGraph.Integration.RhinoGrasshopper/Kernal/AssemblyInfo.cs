using System;
using System.Drawing;

using Grasshopper.Kernel;


namespace BuildingGraph.Integration.RhinoGrasshopper.Kernal
{
    public class AssemblyInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "Neo4j AEC Grasshopper";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return Properties.Resources.HL_Logo24; ;
            }
        }

        public override Bitmap AssemblyIcon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return Properties.Resources.HL_Logo24; ;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "Noe4j Grasshopper nodes";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("CB78B572-38EE-4F71-9B70-79D699015D36");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Will Reynolds at Hoare Lea";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "Will Reynolds -> willreynolds@hoarelea.com";
            }
        }
    }

}
